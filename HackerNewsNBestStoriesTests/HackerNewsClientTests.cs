using FluentAssertions;
using HackerNewsNBestStories.Interface;
using HackerNewsNBestStories.RecordDto;
using HackerNewsNBestStories.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace HackerNewsNBestStoriesTests
{
    [TestFixture]
    public class HackerNewsClientTests
    {
        private IHackerNewsClient _hackerNewsClient;
        private const string BASE_ADDRESS = "https://hacker-news.firebaseio.com/v0/";

        [Test]
        public async Task GetBestStoriesIdsAsync_WhenCalled_ReturnsData()
        {
            //Arrange
           var _logger = new NullLogger<HackerNewsClient>();
            var mockHttpMessageHandler = new MockHttpMessageHandler();
            var output = JsonSerializer.Serialize(new int[] { 37112741, 37125118, 37093854, 37112615, 37091983, 37118883, 37115626 });
            mockHttpMessageHandler
                .When("https://hacker-news.firebaseio.com/v0/beststories.json").Respond("application/json",output.ToString());

            var client = mockHttpMessageHandler.ToHttpClient();
            client.BaseAddress = new Uri(BASE_ADDRESS);
            _hackerNewsClient = new HackerNewsClient(client, _logger);
            

            //Act
            var allBestStoriesIds = await _hackerNewsClient.GetBestStoriesIdsAsync();

            //Assert
            allBestStoriesIds.Should().HaveCount(7);
        }

        [Test]
        public async Task GetStoryDetailByIdAsync_WhenCalled_ReturnsData()
        {
            //Arrange
            int idToTest = 37112741;
            var _logger = new NullLogger<HackerNewsClient>();
            var mockHttpMessageHandler = new MockHttpMessageHandler();
            var output = JsonSerializer.Serialize(new HackerNewsStoryDetail { by = "mikekchar", descendants = 500, id = idToTest , kids = new[] { 123}, score = 500, time = long.MaxValue -10, title = "test" , type = "story", url ="https://www.google.com" });
            mockHttpMessageHandler
                .When("https://hacker-news.firebaseio.com/v0/item/37112741.json").Respond("application/json", output.ToString());

            var client = mockHttpMessageHandler.ToHttpClient();
            client.BaseAddress = new Uri(BASE_ADDRESS);
            _hackerNewsClient = new HackerNewsClient(client, _logger);

            


            //Act
            var storyDetails = await _hackerNewsClient.GetStoryDetailByIdAsync(idToTest);

            //Assert
            storyDetails.id.Should().Be(idToTest);
        }
    }
}
