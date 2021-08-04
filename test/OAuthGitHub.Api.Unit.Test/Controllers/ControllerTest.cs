using Microsoft.Extensions.Logging;
using Moq;

namespace OAuthGitHub.Api.Unit.Test.Controllers
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
