using System.IO;
using BoDi;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.TestFramework;

namespace OAuthGitHub.Integration.Test.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private readonly DefaultTestRunContext _testRunContext;

        public Hooks(DefaultTestRunContext testRunContext)
        {
            _testRunContext = testRunContext;
        }

        [BeforeScenario(Order = 1)]
        public void RegisterDependencies(IObjectContainer objectContainer)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(
                    Path.Combine(_testRunContext.GetTestDirectory(), "appsettings.Test.json"),
                    optional: true, reloadOnChange: true)
                .Build();

            objectContainer.RegisterInstanceAs(config);
        }
    }
}
