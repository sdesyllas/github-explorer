namespace GitHubExplorer.Abstractions
{
    public interface IConverter
    {
        T DeserializeObject<T>(string input);
    }
}
