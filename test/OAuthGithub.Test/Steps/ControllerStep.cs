using TechTalk.SpecFlow;

namespace OAuthGithub.Test.Steps
{
    [Binding]
    public sealed class ControllerStep
    {
        private readonly ScenarioContext _scenarioContext;

        public ControllerStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I send a GET request to ""(.*)""")]
        public void GivenISendAGetRequestTo(string route)
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the response should be:")]
        public void ThenTheResponseShouldBe(string response)
        {
            ScenarioContext.StepIsPending();
        }
    }
}

