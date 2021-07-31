using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OAuthGitHub.Api.Application;
using OAuthGitHub.Api.Extensions;

namespace OAuthGitHub.Api.Infrastructure.Controllers.SignIn
{
    [Route("auth")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly AuthService _authService;

        public SignInController(AuthService authService)
        {
            _authService = authService;
        }

        private IActionResult ProviderNotSupported(string provider)
        {
            ModelState.AddModelError("ProviderNotSupported",
                $"Login provider from {provider} is not supported");
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("signIn/{provider}")]
        public async Task<IActionResult> SignIn([FromRoute] string provider)
        {
            if (!await HttpContext.IsProviderSupportedAsync(provider))
            {
                return ProviderNotSupported(provider);
            }

            return Challenge(new AuthenticationProperties {RedirectUri = "/"}, provider);
        }
    }
}
