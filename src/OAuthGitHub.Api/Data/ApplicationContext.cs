using Microsoft.EntityFrameworkCore;

namespace OAuthGitHub.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
