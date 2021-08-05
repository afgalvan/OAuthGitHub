using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using OAuthGitHub.Api.Unit.Test.Stubs;
using OAuthGithub.Core.Application;

namespace OAuthGitHub.Api.Unit.Test.Mocks
{
    public static class SignUpMediatorMock
    {
        private static readonly Mock<IMediator> Mock = new();

        private static readonly Expression<Func<IMediator, Task<string>>> Expression =
            mediator => mediator.Send(It.IsAny<RegisterCommand>(), new CancellationToken());

        public static IMediator Mediator()
        {
            Mock.Setup(Expression).ReturnsAsync(TokenStub.Token);
            return Mock.Object;
        }

        public static void MockShouldBeCalled(this IMediator mediator)
        {
            Mock.Verify(Expression, Times.AtLeastOnce);
        }
    }
}
