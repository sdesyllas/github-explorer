using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitHubExplorer.BusinessLayer.Model
{
    public class GitHubUser : User
    {
        [JsonProperty("repos_url")]
        public string RepositoriesUrl { get; set; }
        [JsonIgnore]
        public IList<GitHubRepository> GitHubRepositories { get; set; } 
    }
}
