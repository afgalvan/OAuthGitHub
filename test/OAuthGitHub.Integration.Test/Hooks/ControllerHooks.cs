using TechTalk.SpecFlow;

namespace OAuthGitHub.Integration.Test.Hooks
{
    [Binding]
    public sealed class ControllerHooks
    {

        [BeforeScenarioBlock()]
        public static void BeforeScenario()
        {

        }

        [AfterScenario]
        public static void AfterScenario()
        {
        }
    }
}

