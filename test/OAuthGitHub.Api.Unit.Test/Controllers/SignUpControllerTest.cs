using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using OAuthGitHub.Api.Controllers.Shared;
using OAuthGitHub.Api.Controllers.SignUp;
using OAuthGitHub.Api.Unit.Test.Mocks;
using OAuthGitHub.Api.Unit.Test.Stubs;

namespace OAuthGitHub.Api.Unit.Test.Controllers
{
    [TestFixture]
    public class SignUpControllerTest : ControllerTest<SignUpController>
    {
        private SignUpController _controller;
        private ActionResult     _response;
        private IMediator        _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator   = SignUpMediatorMock.Mediator();
            _controller = new SignUpController(_mediator, Logger.Object);
        }

        [Test, Order(0)]
        public async Task RegisterNonExistingUser_ShouldReturnCreatedStatus()
        {
            _response =
                await _controller.SignUp(SignUpRequestStub.Request, new CancellationToken());
            _mediator.Should().HaveBeenCalledMock();

            _response.Should().BeAssignableTo(typeof(CreatedResult))
                .And.NotBeEquivalentTo(typeof(BadRequestResult));
        }

        [Test, Order(3)]
        public void RegisterNonExistingUser_ShouldReturnTheToken()
        {
            var okResponse   = _response.As<CreatedResult>();
            var authResponse = okResponse.Value.As<AuthenticationResponse>();

            authResponse.Token.Should().BeEquivalentTo(TokenStub.Token);
        }
    }
}
