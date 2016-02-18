using Newtonsoft.Json;

namespace GitHubExplorer.BusinessLayer.Model
{
    public class GitHubRepository
    {
        [JsonProperty("name")]
        public string RepositoryName { get; set; }
        [JsonProperty("html_url")]
        public string RepositoryUrl { get; set; }
        [JsonProperty("stargazers_count")]
        public int StarCount { get; set; }
    }
}
