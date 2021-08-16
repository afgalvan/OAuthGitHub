namespace OAuthGitHub.Api.Unit.Test.Controllers

open System.Threading
open FluentAssertions
open FsUnit
open FSharp.Control.Tasks.V2
open Microsoft.AspNetCore.Mvc
open OAuthGitHub.Api.Controllers.Shared
open OAuthGitHub.Api.Unit.Test.Stubs.Stubs
open NUnit.Framework
open OAuthGitHub.Api.Controllers.SignUp
open OAuthGitHub.Api.Unit.Test.Mocks.SignUpMediatorMock

[<TestFixture>]
type SignUpControllerTest() =
    inherit ControllerTest<SignUpController>()
    let mutable response: ActionResult = null

    member private this.mediator = Mediator()

    member private this.controller =
        SignUpController(this.mediator, this.Logger.Object)

    [<Test; Order(0)>]
    member this.RegisterNonExistingUser_ShouldReturnCreatedStatus() =
        task {
            let! r = this.controller.SignUp(signUpRequestStub, CancellationToken.None)
            response <- r
            HaveBeenCalledMock()
            response |> should be ofExactType<CreatedResult>
        }

    [<Test; Order(3)>]
    member this.RegisterNonExistingUser_ShouldReturnTheToken() =
        response |> should not' (equal null)
        let okResponse = response.As<CreatedResult>()

        let authResponse =
            okResponse.Value.As<AuthenticationResponse>()

        authResponse.Token |> should be (sameAs tokenStub)
