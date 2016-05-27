using GitHubExplorer.Abstractions;
using Newtonsoft.Json;

namespace GitHubExplorer.Common
{
    public class JsonConverter : IConverter
    {
        
        public T DeserializeObject<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }
    }
}
