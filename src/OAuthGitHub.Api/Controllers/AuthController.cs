using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OAuthGitHub.Api.Extensions;

namespace OAuthGitHub.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ActionResult Private() => Ok(new
        {
            Name = "Private Route",
        });

        [HttpPost("{provider}")]
        public async Task<ActionResult> SignIn([FromRoute] string provider)
        {
            if (!await HttpContext.IsProviderSupportedAsync(provider))
            {
                return BadRequest("Provider is not supported");
            }
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
        }

        [HttpPost]
        public ActionResult SignUp()
        {
            return Ok();
        }
    }
}
