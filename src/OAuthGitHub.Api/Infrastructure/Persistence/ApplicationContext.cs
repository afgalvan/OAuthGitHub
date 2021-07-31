using Microsoft.EntityFrameworkCore;
using OAuthGitHub.Api.Domain;

namespace OAuthGitHub.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
