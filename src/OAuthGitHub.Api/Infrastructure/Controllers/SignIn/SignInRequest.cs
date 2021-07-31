using System.ComponentModel.DataAnnotations;

namespace OAuthGitHub.Api.Infrastructure.Controllers.SignIn
{
    public class SignInRequest
    {
        [Required]
        public string UsernameOrEmail { get; init; }

        [Required]
        public string Password { get; init; }
    }
}
