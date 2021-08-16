module OAuthGitHub.Api.Unit.Test.Mocks

open System
open System.Linq.Expressions
open System.Threading
open System.Threading.Tasks
open MediatR
open Moq
open OAuthGitHub.Api.Unit.Test.Stubs.Stubs
open OAuthGithub.Core.Application

type Expr =
    static member Quote(e: Expression<Func<IMediator, Task<string>>>) = e

module SignUpMediatorMock =
    let Mock = Mock<IMediator>()

    let Expression =
        Expr.Quote
            (fun mediator -> mediator.Send(It.IsAny<RegisterCommand>(), CancellationToken()))

    let Mediator() =
        let _ = Mock.Setup(Expression).ReturnsAsync(tokenStub)
        Mock.Object

    let HaveBeenCalledMock() =
        Mock.Verify(Expression, Times.AtLeastOnce)
