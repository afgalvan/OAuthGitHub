using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace OAuthGitHub.Api.Extensions
{
    public static class HttpContextExtensions
    {
        private static async Task<AuthenticationScheme[]> GetExternalProvidersAsync(
            this HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var schemes = context.RequestServices
                .GetRequiredService<IAuthenticationSchemeProvider>();

            return (await schemes.GetAllSchemesAsync())
                .Where(scheme => !string.IsNullOrEmpty(scheme.DisplayName))
                .ToArray();
        }

        public static async Task<bool> IsProviderSupportedAsync(this HttpContext context,
            string provider)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return (await context.GetExternalProvidersAsync())
                .Any(scheme =>
                    string.Equals(scheme.Name, provider, StringComparison.OrdinalIgnoreCase)
                );
        }
    }
}
