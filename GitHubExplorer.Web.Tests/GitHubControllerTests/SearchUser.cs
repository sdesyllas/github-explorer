using System.Web.Mvc;
using FizzWare.NBuilder;
using FluentAssertions;
using GitHubExplorer.Abstractions;
using GitHubExplorer.BusinessLayer.Model;
using GitHubExplorer.Web.Controllers;
using GitHubExplorer.Web.Models;
using Moq;
using NUnit.Framework;

namespace GitHubExplorer.Web.Tests.GitHubControllerTests
{
    [TestFixture]
    public class SearchUser
    {
        private GitHubController _gitHubController;
        private Mock<IVcsService<GitHubUser, GitHubRepository>> _mockVcsService;

        [SetUp]
        public void SetUp()
        {
            _mockVcsService = new Mock<IVcsService<GitHubUser, GitHubRepository>>();
            _gitHubController = new GitHubController(_mockVcsService.Object);
        }


        [Test]
        public void ReturnsResult_To_View()
        {
            // Arrange
            GitHubUser actualResult = Builder<GitHubUser>.CreateNew()
                .With(x => x.GitHubRepositories = Builder<GitHubRepository>.CreateListOfSize(100).Build()).Build();
            _mockVcsService.Setup(x => x.GetUser(It.IsAny<string>())).Returns(actualResult);

            // Assign
            var result =
                _gitHubController.SearchUser(new GitHubProfileModel {UserName = actualResult.UserName}) as ViewResult;
            
            // Assert
            _mockVcsService.Verify(x=>x.GetUser(It.IsAny<string>()), Times.Once);
            var model = result?.Model as GitHubProfileModel;

            if (model == null) return;
            model.UserName.Should().Be(actualResult.UserName);
            model.AvatarUrl.Should().Be(actualResult.AvatarUrl);
            model.Location.Should().Be(actualResult.Location);
            model.GitHubRepositories.ShouldAllBeEquivalentTo(actualResult.GitHubRepositories);
        }
    }
}
