using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using RedditChallengeAPI.Controllers;
using RedditChallengeAPI.Models;
using System.Net;

namespace UnitTestRedditChallenge
{    
    public class RedditPostsControllerTests
    {
        private readonly RedditWrapperController _controller;
        private readonly Mock<HttpMessageHandler> _mockMessageHandler;

        public RedditPostsControllerTests()
        {
            _mockMessageHandler = new Mock<HttpMessageHandler>();
            var client = new HttpClient(_mockMessageHandler.Object)
            {
                BaseAddress = new Uri("https://www.reddit.com")
            };

            // Assuming Configuration is set up to read from appsettings.json or similar
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _controller = new RedditWrapperController(client, configuration);
        }

        [Fact]
        public async Task GetPostsAsync_ReturnsOkResult_WithPosts()
        {
            // Arrange
            var subreddit = "testSubreddit";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"data\": {\"children\": [{\"data\": {\"title\": \"Test Post\", \"author_fullname\": \"author1\", \"ups\": 10}}]}}")
            };
            _mockMessageHandler.Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse);

            // Act
            var result = await _controller.GetPostsAsync(subreddit);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var posts = Assert.IsAssignableFrom<IEnumerable<Post>>(okResult.Value);
            Assert.Single(posts);
            Assert.Equal("Test Post", posts.First().Title);
        }

        [Fact]
        public async Task GetPostsAsync_ReturnsStatusCode_WhenRedditApiFails()
        {
            // Arrange
            var subreddit = "testSubreddit";
            _mockMessageHandler.Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            // Act
            var result = await _controller.GetPostsAsync(subreddit);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(503, statusCodeResult.StatusCode);
        }
    }
}