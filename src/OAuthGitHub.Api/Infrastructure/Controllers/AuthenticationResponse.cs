namespace OAuthGitHub.Api.Infrastructure.Controllers
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
