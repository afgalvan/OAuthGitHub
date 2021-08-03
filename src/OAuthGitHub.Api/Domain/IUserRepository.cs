using System.Threading;
using System.Threading.Tasks;

namespace OAuthGitHub.Api.Domain
{
    public interface IUserRepository : IRepository<User, int>
    {
        public Task<User> GetUserByEmailOrUsername(string usernameOrEmail, CancellationToken cancellation);
        public Task RemoveById(int id, CancellationToken cancellation);
    }
}
