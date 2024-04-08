# Reddit API Consumer Project

## Description

This project is a .NET application designed to consume posts from a specified subreddit in near real-time and track specific statistics such as the posts with the most upvotes and the users with the most posts. It uses the Reddit API to fetch posts and provides a Web API to report these statistics.

## Features

- Consumes posts from a specified subreddit using the Reddit API.
- Tracks statistics including posts with the most upvotes and users with the most posts.
- Exposes a Web API endpoint to report tracked statistics.
- Implements rate limiting to comply with Reddit's API usage policies.
- Utilizes asynchronous programming for efficient API consumption.

## Getting Started

### Prerequisites

- .NET 6/7 SDK
- Visual Studio or another .NET-capable IDE
- A Reddit API Key (see Configuration section)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/williejsn23/RedditChallengeSolution.git

   dotnet build
2. Configuration
Acquire a Reddit API Key by registering your application on Reddit.
Add your Reddit API Key and chosen subreddit to the appsettings.json file or use environment variables:
{
  "RedditApi": {
    "ApiKey": "YourApiKeyHere",
    "Subreddit": "YourSubredditHere"
  }
}

3. dotnet run --project Path/To/Your/Project.csproj

