using TechTalk.SpecFlow;

namespace OAuthGitHub.Integration.Test.Hooks
{
    [Binding]
    public sealed class ControllerHooks
    {

        [BeforeScenario]
        public void BeforeScenario()
        {
        }

        [AfterScenario]
        public void AfterScenario()
        {
        }
    }
}
