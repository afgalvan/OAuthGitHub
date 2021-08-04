using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using OAuthGitHub.Api;
using TechTalk.SpecFlow;
using Xunit;

namespace OAuthGitHub.Integration.Test.Steps
{
    [Binding]
    public sealed class ControllerStep : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient          _client;
        private          HttpResponseMessage _response;

        public ControllerStep(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Given(@"I send a GET request to ""(.*)""")]
        public async Task GivenISendAGetRequestTo(string route)
        {
            _response = await _client.GetAsync(route);
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            _response.StatusCode.Should().Be(expectedStatusCode);
        }

        [Then(@"the response should be:")]
        public async Task ThenTheResponseShouldBe(string response)
        {
            string rawActualResponse = await _response.Content.ReadAsStringAsync();

            Task<object> expectedResponseTask =
                Task.Factory.StartNew(() => JsonConvert.DeserializeObject(response));
            Task<object> actualResponseTask =
                Task.Factory.StartNew(() => JsonConvert.DeserializeObject(rawActualResponse));
            await Task.WhenAll(expectedResponseTask, actualResponseTask);

            object expectedResponse = await expectedResponseTask;
            object actualResponse   = await actualResponseTask;

            expectedResponse.Should().BeEquivalentTo(actualResponse);
        }
    }
}
