using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Text;
using dotenv.net;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OAuthGitHub.Api.Application.OpenApiSpec;
using OAuthGitHub.Api.Controllers.SignUp;
using OAuthGithub.Core.Application;
using OAuthGithub.Core.Domain;
using OAuthGithub.Core.Infrastructure.Persistence;

namespace OAuthGitHub.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<SecurityTokenHandler, JwtSecurityTokenHandler>();
            services.AddScoped<IUserRepository, UserMysqlRepository>();
            services.AddScoped<JwtGenerator>();
            services.AddScoped<AccountCreator>();
            services.AddScoped<Hasher>();
            services.AddScoped<ILogger<SignUpController>, Logger<SignUpController>>();
            services.AddMediatR(Assembly.Load("OAuthGitHub.Core"));
        }

        public static void ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
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
                        ValidateLifetime         = true,
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
