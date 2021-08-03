using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthGitHub.Api.Domain;

namespace OAuthGitHub.Api.Infrastructure.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(user => user.Email).IsUnique();
            builder.HasIndex(user => user.Name).IsUnique();
        }
    }
}
