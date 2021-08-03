using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OAuthGitHub.Api.Infrastructure.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Authorize]
        [HttpGet("private")]
        public ActionResult Private() => Ok(new
        {
            Name = "Private Route",
        });

        [Authorize]
        [HttpGet]
        public ActionResult Index() => Ok(new
        {
            Name    = "OAuth with GitHub Example",
            Version = "1.0.0"
        });
    }
}
