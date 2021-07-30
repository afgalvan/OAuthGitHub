using Microsoft.AspNetCore.Builder;

namespace OAuthGitHub.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "OAuthGitHub.Api v1"));
        }
    }
}
