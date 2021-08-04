using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OAuthGitHub.Api.Controllers
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

        [HttpGet]
        public ActionResult Index() => Ok(new
        {
            Name    = "OAuth with GitHub Example",
            Version = "1.0.0"
        });

        [HttpGet("{regex(^doc)}")]
        public ActionResult Documentation() => Redirect("swagger/index.html");
    }
}
