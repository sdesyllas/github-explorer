using System.Web.Mvc;
using GitHubExplorer.Abstractions;
using GitHubExplorer.BusinessLayer.Model;
using GitHubExplorer.Web.Models;

namespace GitHubExplorer.Web.Controllers
{
    public class GitHubController : Controller
    {
        public readonly IVcsService<GitHubUser, GitHubRepository> _ivcsService;

        public GitHubController(IVcsService<GitHubUser, GitHubRepository> ivcsService)
        {
            _ivcsService = ivcsService;
        }

        // GET: GitHub
        [HttpGet]
        public ActionResult SearchUser()
        {
            return View(new GitHubProfileModel());
        }

        [HttpPost]
        public ActionResult SearchUser(GitHubProfileModel model)
        {
            var gitHubUser = _ivcsService.GetUser(model.UserName);
            model.AvatarUrl = gitHubUser.AvatarUrl;
            model.Location = gitHubUser.Location;
            model.GitHubRepositories = gitHubUser.GitHubRepositories;
            return View(model);
        }
    }
}