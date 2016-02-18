namespace GitHubExplorer.Abstractions
{
    public interface IWebClient
    {
        string DownloadString(string url);
    }
}
