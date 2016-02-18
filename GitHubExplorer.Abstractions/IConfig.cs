using System.Security.Cryptography.X509Certificates;

namespace GitHubExplorer.Abstractions
{
    public interface IConfig
    {
        string GitHubUrl { get; }

        int NumberOfReposToShow { get; }
    }
}
