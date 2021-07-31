using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OAuthGitHub.Api.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index() => Ok(new
        {
            Name    = "OAuth with GitHub Example",
            Version = "1.0.0"
        });
    }
}
