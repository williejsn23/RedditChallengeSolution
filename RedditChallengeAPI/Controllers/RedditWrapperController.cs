using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RedditChallengeAPI.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedditChallengeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedditWrapperController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _redditApiBaseUrl;

        public RedditWrapperController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _redditApiBaseUrl = configuration["RedditApi:BaseUrl"];
        }

        // GET: api/RedditPosts/{subreddit}
        [HttpGet("{subreddit}")]
        public async Task<IActionResult> GetPostsAsync(string subreddit)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_redditApiBaseUrl}{subreddit}/new.json?limit=100");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Failed to retrieve data from Reddit.");
                }

                var content = await response.Content.ReadAsStringAsync();
                var redditResponse = JsonConvert.DeserializeObject<RedditResponse>(content);

                var posts = redditResponse?.Data?.Children?.Select(c => c.Data)?.ToList() ?? new List<RedditPost>();
                return Ok(posts);
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Service unavailable. Unable to fetch data from Reddit.");
            }
        }
    }
}
