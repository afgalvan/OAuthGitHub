using OAuthGitHub.Api.Controllers.SignUp;

namespace OAuthGithub.Api.UnitTest.Stubs
{
    public static class SignUpRequestStub
    {
        public static readonly SignUpRequest Request = new()
        {
            Username = "testUser",
            Email = "testUser@example.com",
            Password = "testPassword"
        };
    }
}
