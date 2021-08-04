using Microsoft.Extensions.Logging;
using Moq;

namespace OAuthGitHub.Api.Unit.Test.Controllers
{
    public class ControllerTest<T>
    {
        protected Mock<ILogger<T>> Logger { get; }

        protected ControllerTest()
        {
            Logger = new Mock<ILogger<T>>();
        }
    }
}
