using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExplorer.Abstractions;
using GitHubExplorer.BusinessLayer.Model;
using Moq;
using NUnit.Framework;

namespace GitHubExplorer.BusinessLayer.Tests.GitHubService
{
    [TestFixture]
    public class GetRepositories
    {
        private IVcsService<GitHubUser, GitHubRepository> _ivcsService;

        private Mock<IWebClient> _mockWebClient;
        private Mock<IConfig> _mockConfig;
        private Mock<IConverter> _mockConverter;

        [SetUp]
        public void SetUp()
        {
            _mockConverter = new Mock<IConverter>();
            _mockWebClient = new Mock<IWebClient>();
            _mockConfig = new Mock<IConfig>();
            _ivcsService = new BusinessLayer.GitHubService(_mockConfig.Object, _mockWebClient.Object, _mockConverter.Object);
        }

        [Test]
        public void Calls_GitHubApi()
        {
            // Arrange
            const string url = "http://something";
            _mockWebClient.Setup(x => x.DownloadString(url)).Returns(string.Empty).Verifiable();

            // Act
            _ivcsService.GetRepositories(url);

            // Assert
            _mockWebClient.Verify();
        }

        [Test]
        public void Returns_DeserializedRepositories()
        {
            const string jsonRepositories = "something";
            _mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(jsonRepositories);
            _mockConverter.Setup(x => x.DeserializeObject<List<GitHubRepository>>(jsonRepositories))
                .Returns(new List<GitHubRepository>())
                .Verifiable();

            _ivcsService.GetRepositories("http://something");
            _mockWebClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
            _mockConverter.Verify(x=>x.DeserializeObject<List<GitHubRepository>>(It.IsAny<string>()), Times.Once);

            _mockConverter.Verify();
        }
    }
}
