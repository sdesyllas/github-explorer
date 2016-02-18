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
        public void Controller_Calls_SearchUser_From_Service()
        {
            // Arrange
            var model = Builder<GitHubProfileModel>.CreateNew().Build();
            _mockVcsService.Setup(x => x.GetUser(model.UserName)).Returns(new GitHubUser()).Verifiable();

            // Assign
            _gitHubController.SearchUser(model);

            // Assert
            _mockVcsService.Verify();
        }

        [Test]
        public void ReturnsResult_To_View()
        {
            // Arrange
            GitHubUser actualResult = new GitHubUser();
            _mockVcsService.Setup(x => x.GetUser(It.IsAny<string>())).Returns(actualResult);

            // Assign
            var result = _gitHubController.SearchUser(new GitHubProfileModel()) as ViewResult;

            // Assert
            result.Model.As<GitHubProfileModel>().UserName.Should().Be(actualResult.UserName);
            result.Model.As<GitHubProfileModel>().AvatarUrl.Should().Be(actualResult.AvatarUrl);
            result.Model.As<GitHubProfileModel>().Location.Should().Be(actualResult.Location);
            result.Model.As<GitHubProfileModel>().GitHubRepositories.ShouldAllBeEquivalentTo(actualResult.GitHubRepositories);
        }
    }
}
