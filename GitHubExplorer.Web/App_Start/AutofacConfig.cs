using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using GitHubExplorer.Abstractions;
using GitHubExplorer.BusinessLayer;
using GitHubExplorer.BusinessLayer.Configurations;
using GitHubExplorer.BusinessLayer.Model;
using GitHubExplorer.Common;

namespace GitHubExplorer.Web.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<Config>().As<IConfig>();
            builder.RegisterType<JsonConverter>().As<IConverter>().InstancePerRequest();
            builder.RegisterType<CustomWebClient>().As<IWebClient>().InstancePerRequest();
            builder.RegisterType<GitHubService>().As<IVcsService<GitHubUser, GitHubRepository>>().InstancePerRequest();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof (MvcApplication).Assembly);

            
            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            /*
            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();*/

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}