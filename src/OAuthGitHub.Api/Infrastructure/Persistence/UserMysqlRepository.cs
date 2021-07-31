using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OAuthGitHub.Api.Domain;

namespace OAuthGitHub.Api.Infrastructure.Persistence
{
    public class UserMysqlRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserMysqlRepository(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }

        public async Task<User> Save(User entity, CancellationToken cancellation)
        {
            await _context.Users.AddAsync(entity, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return entity;
        }

        public async Task<IEnumerable<User>> GetAll() => await _context.Users.ToListAsync();

        public async Task<User> GetById(int id) => await _context.Users.FindAsync(id);

        public async Task Remove(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailOrUsername(string usernameOrEmail)
        {
            return (await GetAll()).FirstOrDefault(user =>
                user.Email == usernameOrEmail || user.Name == usernameOrEmail);
        }

        public async Task RemoveById(int id)
        {
            User user = await GetById(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
