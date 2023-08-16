using FluentAssertions;
using HackerNewsNBestStories.Interface;
using HackerNewsNBestStories.Service;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace HackerNewsNBestStoriesTests
{
    [TestFixture]
    public class HackerNewsClientTests
    {
        private readonly object API_TEST_CONTENT_BEST_STORIES_IDS = @"{[37112741,37125118,37093854,37112615,37091983,37118883,37115626]}";
        private IHackerNewsClient _hackerNewsClient;
        private ILogger<HackerNewsClient> _logger;

        [Test]
        public async Task GetBestStoriesIdsAsync_WhenCalled_ReturnsData()
        {
            //Arrange
            var _logger = new Mock<ILogger<HackerNewsClient>>();
            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler
                .When("https://hacker-news.firebaseio.com/v0/beststories.json")
                .Respond("application/json", "{[37112741,37125118,37093854,37112615,37091983,37118883,37115626]}");

            var client = mockHttpMessageHandler.ToHttpClient();
            _hackerNewsClient = new HackerNewsClient(client, (ILogger<HackerNewsClient>)_logger);

            //Act
            var allBestStoriesIds = await _hackerNewsClient.GetBestStoriesIdsAsync();

            //Assert
            allBestStoriesIds.Should().HaveCount(9);
        }

        [Test]
        public void Test_case()
        {
            Assert.IsTrue(true);
        }
    }
}
