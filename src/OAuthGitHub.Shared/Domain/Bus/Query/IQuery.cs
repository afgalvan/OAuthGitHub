using MediatR;

namespace OAuthGitHub.Shared.Domain.Bus.Query
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
