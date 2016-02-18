using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GitHubExplorer.BusinessLayer.Model;

namespace GitHubExplorer.Web.Models
{
    public class GitHubProfileModel
    {
        public GitHubProfileModel()
        {
            this.GitHubRepositories = new List<GitHubRepository>();
        }

        [Required]
        public string UserName { get; set; }
        public string Location { get; set; }
        public string AvatarUrl { get; set; }

        public IList<GitHubRepository> GitHubRepositories { get; set; } 
    }
}