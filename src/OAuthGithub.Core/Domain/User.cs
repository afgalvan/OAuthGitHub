using System.ComponentModel.DataAnnotations;

namespace OAuthGitHub.Api.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public User(string name, string email, string password)
        {
            Name     = name;
            Email    = email;
            Password = password;
        }
    }
}
