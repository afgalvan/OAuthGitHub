using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using OAuthGitHub.Api;
using OAuthGitHub.Api.Controllers.SignUp;
using TechTalk.SpecFlow;
using Xunit;

namespace OAuthGitHub.Integration.Test.Steps
{
    [Binding]
    public sealed class ControllerSteps : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient          _client;
        private          HttpResponseMessage _response;
        private          HttpResponseMessage _request;

        public ControllerSteps(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Given(@"I send a POST request to ""(.*)"" with body:")]
        public async Task GivenISendAPostRequestToWithBody(string route, string body)
        {
            await PostRequest(route, body);
        }

        [When(@"I send a POST request to ""(.*)"" with body:")]
        public async Task WhenISendAPostRequestToWithBody(string route, string body)
        {
            await PostRequest(route, body);
        }

        private async Task PostRequest(string route, string bodyString)
        {
            SignUpRequest body =
                await Task.Factory.StartNew(() =>
                    JsonConvert.DeserializeObject<SignUpRequest>(bodyString));
            HttpContent content = JsonContent.Create(body);

            _request = await _client.PostAsync(route, content);
            Console.WriteLine(new string('-', 50));
            Console.WriteLine(body.ToString());
            Console.WriteLine(new string('-', 50));

            _response = _request;
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
