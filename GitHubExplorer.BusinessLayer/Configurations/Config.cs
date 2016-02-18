using System.Configuration;
using GitHubExplorer.Abstractions;

namespace GitHubExplorer.BusinessLayer.Configurations
{
    public class Config : IConfig
    {
        public string GitHubUrl => string.IsNullOrEmpty(ConfigurationManager.AppSettings["gitHub.URL"])
            ? string.Empty
            : ConfigurationManager.AppSettings["gitHub.URL"];

        public int NumberOfReposToShow => string.IsNullOrEmpty(ConfigurationManager.AppSettings["gitHub.NumberOfReposToShow"])
            ? 0
            : int.Parse(ConfigurationManager.AppSettings["gitHub.NumberOfReposToShow"]);
    }
}
