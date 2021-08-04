using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OAuthGithub.Core.Domain;

namespace OAuthGithub.Core.Infrastructure.Persistence
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

        public async Task<IEnumerable<User>> GetAll(CancellationToken cancellation) =>
            await _context.Users.ToListAsync(cancellation);

        public async Task<User> GetById(int id, CancellationToken cancellation) =>
            await _context.Users.FindAsync(new object[] {id}, cancellationToken: cancellation);

        public async Task Remove(User entity, CancellationToken cancellation)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task<User> GetUserByEmailOrUsername(string usernameOrEmail,
            CancellationToken cancellation)
        {
            return (await GetAll(cancellation)).FirstOrDefault(user =>
                user.Email == usernameOrEmail || user.Name == usernameOrEmail);
        }

        public async Task RemoveById(int id, CancellationToken cancellation)
        {
            User user = await GetById(id, cancellation);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellation);
        }
    }
}
