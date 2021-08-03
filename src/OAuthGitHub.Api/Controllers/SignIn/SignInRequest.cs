using System.ComponentModel.DataAnnotations;

namespace OAuthGitHub.Api.Controllers.SignIn
{
    public class SignInRequest
    {
        [Required]
        public string UsernameOrEmail { get; init; }

        [Required]
        public string Password { get; init; }
    }
}
