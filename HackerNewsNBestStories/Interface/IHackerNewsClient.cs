using HackerNewsNBestStories.RecordDto;
using System.Collections.Generic;

namespace HackerNewsNBestStories.Interface
{
    public interface IHackerNewsClient
    {
       Task<int[]> GetBestStoriesIdsAsync();

       Task<HackerNewsStoryDetail> GetStoryDetailByIdAsync(int id);
    }
}
