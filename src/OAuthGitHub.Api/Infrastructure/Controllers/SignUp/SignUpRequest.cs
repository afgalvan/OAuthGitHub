using System.ComponentModel.DataAnnotations;

namespace OAuthGitHub.Api.Infrastructure.Controllers.SignUp
{
    public class SignUpRequest
    {
        [Required]
        public string Username { get; init; }

        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
    }
}
