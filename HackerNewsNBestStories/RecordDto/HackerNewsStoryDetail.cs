namespace HackerNewsNBestStories.RecordDto
{
    public record HackerNewsStoryDetail
    {
        public string by { get; init; }
        public int descendants { get; init; }
        public int id { get; init; }
        public int[] kids { get; init; }
        public int score { get; init; }
        public long time { get; init; }
        public string title { get; init; }
        public string type { get; init; }
        public string url { get; init; }
    }
}
