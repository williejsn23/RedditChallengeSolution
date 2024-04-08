using RedditChallengeAPI.Models;
using System.Collections.Concurrent;

namespace RedditChallengeAPI.Services
{
    public class PostTrackerService : IPostTrackerService
    {
        private readonly ConcurrentDictionary<string, int> _userPostCounts = new ConcurrentDictionary<string, int>();
        private readonly ConcurrentBag<Post> _posts = new ConcurrentBag<Post>();

        public void TrackPost(Post post)
        {
            _posts.Add(post);
            _userPostCounts.AddOrUpdate(post.Author, 1, (key, oldValue) => oldValue + 1);
        }

        public Post GetTopPostByUpvotes()
        {
            return _posts.OrderByDescending(p => p.Upvotes).FirstOrDefault();
        }

        public (string Username, int PostCount) GetTopPoster()
        {
            var top = _userPostCounts.OrderByDescending(kvp => kvp.Value).FirstOrDefault();
            return (top.Key, top.Value);
        }
    }
}
