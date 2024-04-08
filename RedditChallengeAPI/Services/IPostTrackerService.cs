using RedditChallengeAPI.Models;

namespace RedditChallengeAPI.Services
{
    public interface IPostTrackerService
    {
        /// <summary>
        /// Used to track posts made by a user
        /// </summary>
        /// <param name="post"></param>
        void TrackPost(Post post);
        /// <summary>
        /// Used to retrieve posts with the highest upvotes
        /// </summary>
        /// <returns></returns>
        Post GetTopPostByUpvotes();
        /// <summary>
        /// Returns top posters
        /// </summary>
        /// <returns>A tuple showing the Username and Post Count</returns>
        (string Username, int PostCount) GetTopPoster();
    }
}
