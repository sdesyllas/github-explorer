using System.Collections.Generic;

namespace GitHubExplorer.Abstractions
{
    /// <summary>
    /// Version control system
    /// </summary>
    public interface IVcsService<out TUser, TRepository>
    {
        /// <summary>
        /// Search username at GitHub and returns the found user along with their repositories
        /// </summary>
        /// <param name="userName">username to search for</param>
        /// <returns>GitHubUser</returns>
        TUser GetUser(string userName);

        /// <summary>
        /// Gets a list of user repositories
        /// </summary>
        /// <param name="url">url of the repositories</param>
        /// <returns>a list of repositories</returns>
        IList<TRepository> GetRepositories(string url);
    }
}
