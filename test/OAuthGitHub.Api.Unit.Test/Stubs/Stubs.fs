namespace OAuthGitHub.Api.Unit.Test.Stubs

module Stubs =
    open OAuthGitHub.Api.Controllers.SignUp

    let tokenStub = "test_token"

    let signUpRequestStub =
        SignUpRequest("test", "test@example.com", "testPassword")
