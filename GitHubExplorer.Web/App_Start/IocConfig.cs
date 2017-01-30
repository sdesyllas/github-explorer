using System.Reflection;
using System.Web.Mvc;
using GitHubExplorer.Abstractions;
using GitHubExplorer.BusinessLayer;
using GitHubExplorer.BusinessLayer.Configurations;
using GitHubExplorer.BusinessLayer.Model;
using GitHubExplorer.Common;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace GitHubExplorer.Web
{
    public static class IocConfig
    {
        public static void RegisterDependencies()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Register<IConfig, Config>();
            container.Register<IConverter, JsonConverter>();
            container.Register<IWebClient, CustomWebClient>();
            container.Register<IVcsService<GitHubUser, GitHubRepository>, GitHubService>();
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}