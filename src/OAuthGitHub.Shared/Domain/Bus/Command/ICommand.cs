using MediatR;

namespace OAuthGitHub.Shared.Domain.Bus.Command
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
