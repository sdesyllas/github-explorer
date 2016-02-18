using Newtonsoft.Json;

namespace GitHubExplorer.BusinessLayer.Model
{
    public class User
    {
        [JsonProperty("login")]
        public string UserName { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}