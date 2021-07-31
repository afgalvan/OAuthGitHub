using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OAuthGitHub.Api.Application;
using OAuthGitHub.Api.Domain;
using OAuthGitHub.Api.Infrastructure.Controllers.SignUp;
using OAuthGitHub.Api.Infrastructure.Persistence;
using OAuthGitHub.Api.OpenApiSpec;

namespace OAuthGitHub.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<SecurityTokenHandler, JwtSecurityTokenHandler>();
            services.AddScoped<IUserRepository, UserMysqlRepository>();
            services.AddScoped<JwtGenerator>();
            services.AddScoped<AuthService>();
            services.AddScoped<Hasher>();
            services.AddScoped<ILogger<SignUpController>, Logger<SignUpController>>();
        }

        public static void ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            );
        }

        public static void ConfigureProxy(this IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto;
                options.KnownProxies.Add(IPAddress.Parse("0.0.0.0"));
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name         = "Authorization",
                BearerFormat = "JWT",
                Scheme       = AuthenticationScheme.Bearer,
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In   = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Reference = new OpenApiReference
                {
                    Id   = AuthenticationScheme.Bearer,
                    Type = ReferenceType.SecurityScheme
                }
            };

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AuthorizationOperationFilter>();
                options.SwaggerDoc("v1",
                    new OpenApiInfo {Title = "OAuthGitHub", Version = "v1"});
                options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            });
        }

        public static void AddOAuth(this IServiceCollection services)
        {
            IDictionary<string, string> env = DotEnv.Read();

            byte[] key = Encoding.UTF8.GetBytes(env["JWT_SECRET"]);

            services.AddSingleton(_ => new SecretKey(key));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey         = new SymmetricSecurityKey(key),
                        ValidateIssuer           = false,
                        ValidateAudience         = false
                    };
                })
                .AddGitHub(options =>
                {
                    options.ClientId     = env["GITHUB_CLIENT_ID"];
                    options.ClientSecret = env["GITHUB_CLIENT_SECRET"];
                    options.Scope.Add("user:email");
                });
        }
    }
}
