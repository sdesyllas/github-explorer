using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using GitHubExplorer.Abstractions;
using GitHubExplorer.BusinessLayer.Model;
using Moq;
using NUnit.Framework;

namespace GitHubExplorer.BusinessLayer.Tests.GitHubService
{
    [TestFixture]
    public class GetUser
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
            const string gitUrl = "http://something";
            _mockConfig.SetupGet(x => x.GitHubUrl).Returns(gitUrl);
            _mockWebClient.Setup(x => x.DownloadString(gitUrl)).Returns(string.Empty).Verifiable();
            _mockConverter.Setup(x => x.DeserializeObject<GitHubUser>(It.IsAny<string>())).Returns(new GitHubUser());
            _mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns("someRepositories");
            _mockConverter.Setup(x => x.DeserializeObject<List<GitHubRepository>>(It.IsAny<string>())).Returns(new List<GitHubRepository>());

            // Act
            _ivcsService.GetUser(string.Empty);

            // Assert
            _mockWebClient.Verify(x => x.DownloadString(gitUrl));
        }

        [Test]
        public void ReturnsUser_WithRepositories()
        {
            var user = Builder<GitHubUser>.CreateNew().Build();
            var repositories = Builder<GitHubRepository>.CreateListOfSize(3).Build().ToList();
            _mockConfig.SetupGet(x => x.GitHubUrl).Returns("url");
            _mockConfig.SetupGet(x => x.NumberOfReposToShow).Returns(661);
            _mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns("someone");
            _mockConverter.Setup(x => x.DeserializeObject<GitHubUser>(It.IsAny<string>())).Returns(user);
            _mockWebClient.Setup(x => x.DownloadString(user.RepositoriesUrl)).Returns("someRepositories");
            _mockConverter.Setup(x => x.DeserializeObject<List<GitHubRepository>>(It.IsAny<string>())).Returns(repositories);

            _ivcsService.GetUser("someone");

            user.GitHubRepositories.ShouldAllBeEquivalentTo(repositories);
        }

        [Test]
        public void Returns_DeserializedUser()
        {
            const string jsonUser = "something";
            var user = Builder<GitHubUser>.CreateNew().With(x => x.RepositoriesUrl = "http://dddd").Build();
            _mockConfig.SetupGet(x => x.GitHubUrl).Returns("url");
            _mockConfig.SetupGet(x => x.NumberOfReposToShow).Returns(661);
            _mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(jsonUser);
            _mockConverter.Setup(x => x.DeserializeObject<GitHubUser>(jsonUser)).Returns(user).Verifiable();
            _mockWebClient.Setup(x => x.DownloadString(user.RepositoriesUrl)).Returns("someRepositories");
            _mockConverter.Setup(x => x.DeserializeObject<List<GitHubRepository>>(It.IsAny<string>())).Returns(new List<GitHubRepository>());

            _ivcsService.GetUser("someone");

            _mockConverter.Verify();
        }

        [Test]
        public void DoesNotQueryRepositories_WhenUserDoesNotExist()
        {
            GitHubUser user = null;
            _mockConfig.SetupGet(x => x.GitHubUrl).Returns("url");
            _mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns("fffff");
            _mockConverter.Setup(x => x.DeserializeObject<GitHubUser>(It.IsAny<string>())).Returns(user);

            _ivcsService.GetUser("someone");

            _mockWebClient.Verify(x => x.DownloadString(It.IsAny<string>()), Times.Once);
        }
    }
}
