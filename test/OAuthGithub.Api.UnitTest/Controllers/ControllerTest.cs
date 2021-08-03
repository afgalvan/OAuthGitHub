using Microsoft.Extensions.Logging;
using Moq;

namespace OAuthGithub.Api.UnitTest.Controllers
{
    public class ControllerTest<T>
    {
        protected readonly Mock<ILogger<T>> Logger;

        protected ControllerTest()
        {
            Logger = new Mock<ILogger<T>>();
        }
    }
}
