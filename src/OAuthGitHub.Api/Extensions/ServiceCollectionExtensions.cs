using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OAuthGitHub.Api.Data;
using OAuthGitHub.Api.OpenApiSpec;

namespace OAuthGitHub.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("MySQL"))
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

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<AuthorizationOperationFilter>();
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                        {Title = "OAuthGitHub", Version = "v1"});
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            });
        }
    }
}
