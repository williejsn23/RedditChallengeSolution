namespace RedditChallengeAPI.Models
{
    public class RedditResponse
    {
        public string Kind { get; set; }
        public RedditData Data { get; set; }
    }

    public class RedditData
    {
        public string After { get; set; }
        public int Dist { get; set; }
        public string Modhash { get; set; }
        public string GeoFilter { get; set; }
        public List<RedditPostContainer> Children { get; set; }
    }

    public class RedditPostContainer
    {
        public string Kind { get; set; }
        public RedditPost Data { get; set; }
    }

    public class RedditPost
    {
        public string Subreddit { get; set; }
        public string AuthorFullname { get; set; }
        public string Title { get; set; }
        public string Selftext { get; set; }
        public double UpvoteRatio { get; set; }
        public int Ups { get; set; }
        public int Downs { get; set; }
        public int Score { get; set; }
        public string Permalink { get; set; }
        public string Url { get; set; }
        public double CreatedUtc { get; set; }
    }

}
