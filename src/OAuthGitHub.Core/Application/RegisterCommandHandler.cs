using System.Threading;
using System.Threading.Tasks;
using OAuthGitHub.Shared.Domain.Bus.Command;

namespace OAuthGithub.Core.Application
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, string>
    {
        private readonly AccountCreator _accountCreator;

        public RegisterCommandHandler(AccountCreator accountCreator)
        {
            _accountCreator = accountCreator;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _accountCreator.Create(request.Username, request.Email, request.Password, cancellationToken);
        }
    }
}
