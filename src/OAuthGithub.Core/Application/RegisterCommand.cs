using OAuthGitHub.Shared.Domain.Bus.Command;

namespace OAuthGithub.Core.Application
{
    public class RegisterCommand : ICommand<string>
    {
        public string Username     { get; set; }
        public string Email    { get; set; }
        public string Password { get; set; }

        public RegisterCommand(string username, string email, string password)
        {
            Username     = username;
            Email    = email;
            Password = password;
        }
    }
}
