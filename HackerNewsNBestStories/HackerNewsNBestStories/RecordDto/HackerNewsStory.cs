namespace HackerNewsNBestStories.RecordDto
{
    public record HackerNewsStory
    {

        public string title { get; init; }

        public string uri { get; init; }

        public string postedBy { get; init; }

        public string time { get; init; }

        public int score { get; init; }

        public int commentCount { get; init; }
    }
}