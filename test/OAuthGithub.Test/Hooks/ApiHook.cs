using TechTalk.SpecFlow;

namespace OAuthGithub.Test.Hooks
{
    [Binding]
    public sealed class ApiHook
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}

