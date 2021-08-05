using BoDi;
using OAuthGitHub.Integration.Test.Mocks;
using TechTalk.SpecFlow;

namespace OAuthGitHub.Integration.Test.Hooks
{
    [Binding]
    public sealed class ApplicationHooks
    {
        private static TestDbContext _context;

        [BeforeScenario(Order = 1)]
        public static void BeforeScenario(IObjectContainer container)
        {
            _context = new TestDbContext();
            container.RegisterInstanceAs(_context);
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            _context.Database.EnsureDeleted();
        }
    }
}

