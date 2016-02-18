using System.Collections.Generic;
using System.Linq;
using GitHubExplorer.Abstractions;
using GitHubExplorer.BusinessLayer.Model;

namespace GitHubExplorer.BusinessLayer
{
    public class GitHubService : IVcsService<GitHubUser, GitHubRepository>
    {
        private readonly IConfig _config;
        private readonly IWebClient _webClient;
        private readonly IConverter _converter; 

        public GitHubService(IConfig config, IWebClient webClient, IConverter converter)
        {
            _config = config;
            _webClient = webClient;
            _converter = converter;
        }

        public GitHubUser GetUser(string userName)
        {
            var jsonResult = _webClient.DownloadString(string.Format(_config.GitHubUrl, userName));
            var gitHubUser = _converter.DeserializeObject<GitHubUser>(jsonResult);

            if (gitHubUser == null)
            {
                return new GitHubUser();
            }

            gitHubUser.GitHubRepositories =
                GetRepositories(gitHubUser.RepositoriesUrl)
                    .OrderByDescending(x => x.StarCount)
                    .Take(_config.NumberOfReposToShow)
                    .ToList();

            return gitHubUser;
        }

        public IList<GitHubRepository> GetRepositories(string url)
        {
            var jsonResult = _webClient.DownloadString(url);
            return _converter.DeserializeObject<List<GitHubRepository>>(jsonResult);
        }
    }
}
