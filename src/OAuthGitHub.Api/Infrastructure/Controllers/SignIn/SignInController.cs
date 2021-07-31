using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public ActionResult Private() => Ok(new
        {
            Name = "Private Route",
        });

        [HttpPost("signIn/{provider}")]
        public async Task<ActionResult> SignIn([FromRoute] string provider)
        {
            if (!await HttpContext.IsProviderSupportedAsync(provider))
            {
                return BadRequest("Provider is not supported");
            }

            return Challenge(new AuthenticationProperties {RedirectUri = "/"}, provider);
        }
    }
}
