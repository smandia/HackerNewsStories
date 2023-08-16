using HackerNewsNBestStories.Interface;
using HackerNewsNBestStories.RecordDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;

namespace HackerNewsNBestStories.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestStoriesController : ControllerBase
    {
        private readonly ILogger<BestStoriesController> _logger;
        private IHackerNewsClient _hackerNewsClient;

        public BestStoriesController(ILogger<BestStoriesController> logger, IHackerNewsClient hackerNewsClient )
        {
            _logger = logger;
            _hackerNewsClient = hackerNewsClient;
        }

        [HttpGet(Name = "BestStories")]
        public async Task<HackerNewsStory[]> GetAsync(int count)
        {
           var allBestStoriesIds = await _hackerNewsClient.GetBestStoriesIdsAsync();
            var nBestStoryIds = allBestStoriesIds.Take(count);
            ConcurrentBag<HackerNewsStory> ourStoryDetailBag = new ConcurrentBag<HackerNewsStory>();

            CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;
            po.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
          
            await Parallel.ForEachAsync(nBestStoryIds, po, async (id, token) =>
            {
                var storydetail = await _hackerNewsClient.GetStoryDetailByIdAsync(id);
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds(storydetail.time).ToLocalTime();
                var consumerStoryDetailFormat = new HackerNewsStory { title = storydetail.title, uri = storydetail.url, postedBy = storydetail.by, time = dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), score = storydetail.score, commentCount = storydetail.descendants };
                ourStoryDetailBag.Add(consumerStoryDetailFormat);
            });

            var result = ourStoryDetailBag.OrderByDescending(x => x.score).ToArray();
            return result;

        }
    }
}
