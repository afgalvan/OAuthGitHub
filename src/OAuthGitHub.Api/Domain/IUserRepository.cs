using System.Threading.Tasks;

namespace OAuthGitHub.Api.Domain
{
    public interface IUserRepository : IRepository<User, int>
    {
        public Task<User> GetUserByEmailOrUsername(string usernameOrEmail);
        public Task RemoveById(int id);
    }
}
