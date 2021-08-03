namespace OAuthGitHub.Api.Controllers
{
    public class AuthenticationResponse
    {
        public string Token { get; init; }

        public AuthenticationResponse(string token)
        {
            Token = token;
        }
    }
}
