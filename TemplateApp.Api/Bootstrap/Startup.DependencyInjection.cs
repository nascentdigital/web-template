using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using System.Reflection;
using System.Web.Http;
using TemplateApp.Api.Services;
using TemplateApp.Entities;


namespace TemplateApp.Api.Bootstrap
{
    public partial class Startup
    {
        #region helper methods

        private void ConfigureDependencies(IAppBuilder app, 
            HttpConfiguration config)
        {
            // configure container
            ContainerBuilder builder = new ContainerBuilder();

            // bind all webapi controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                .InstancePerRequest();

            // register context
            builder.RegisterType<TemplateAppContext>()
                .AsSelf()
                .InstancePerRequest();

            // register services
            builder.RegisterType<ConfigurationService>()
                .As<IConfigurationService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerRequest();

            // create container
            IContainer container = builder.Build();

            // bind app to autofac
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
        }

        #endregion
    }
}