using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using GitHubExplorer.Abstractions;

namespace GitHubExplorer.Common
{
    public class CustomWebClient : IWebClient
    {
        public string DownloadString(string url)
        {
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(url).Result;

                return response.IsSuccessStatusCode ? response.Content.ReadAsStringAsync().Result : string.Empty;
            }
        }
    }
}
