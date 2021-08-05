using OAuthGitHub.Api.Controllers.SignUp;

namespace OAuthGitHub.Api.Unit.Test.Stubs
{
    public static class SignUpRequestStub
    {
        public static readonly SignUpRequest Request = new()
        {
            Username = "test",
            Email = "testUser@example.com",
            Password = "testPassword"
        };
    }
}
