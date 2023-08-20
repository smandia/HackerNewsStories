using HackerNewsNBestStories.Interface;
using HackerNewsNBestStories.RecordDto;

namespace HackerNewsNBestStories.Service
{
    public class HackerNewsClient : IHackerNewsClient
    {
        private readonly HttpClient _httpClient;
        private const string BEST_STORIES_URI = "beststories.json";
        private readonly ILogger<HackerNewsClient> _logger;

        public HackerNewsClient(HttpClient httpClient, ILogger<HackerNewsClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<int[]> GetBestStoriesIdsAsync()
        {
            try
            {
                var url = BEST_STORIES_URI;
                var bestStoriesIds = await _httpClient.GetFromJsonAsync<int[]>(url);
                return bestStoriesIds ?? Array.Empty<int>();
            }
            catch (Exception ex)
            {
                //log the exception 
                _logger.LogError(ex.Message);
                return Array.Empty<int>();
            }
        }

        public async Task<HackerNewsStoryDetail> GetStoryDetailByIdAsync(int id)
        {
            try
            {
                var uri = $"item/{id}.json";
                var storyDetails = await _httpClient.GetFromJsonAsync<HackerNewsStoryDetail>(uri);
                return storyDetails;
            }
            catch (Exception ex)
            {
                //log the exception 
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
