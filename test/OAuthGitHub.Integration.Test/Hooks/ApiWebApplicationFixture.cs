using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OAuthGitHub.Api;
using OAuthGithub.Core.Infrastructure.Persistence;
using OAuthGitHub.Integration.Test.Mocks;

namespace OAuthGitHub.Integration.Test.Hooks
{
    public class ApiWebApplicationFixture : WebApplicationFactory<Startup>
    {
        private IConfiguration Configuration { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Test.json")
                    .Build();
                config.AddConfiguration(Configuration);
            });
            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<ApplicationDbContext, TestDbContext>();
            });
        }
    }
}
