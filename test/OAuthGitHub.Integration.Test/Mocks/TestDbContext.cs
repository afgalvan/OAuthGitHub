using Microsoft.EntityFrameworkCore;
using OAuthGithub.Core.Infrastructure.Persistence;

namespace OAuthGitHub.Integration.Test.Mocks
{
    public class TestDbContext : ApplicationDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=test.db");
        }
    }
}
