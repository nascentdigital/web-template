using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Web.Http;


[assembly: OwinStartup(typeof(TemplateApp.Api.Bootstrap.Startup))]

namespace TemplateApp.Api.Bootstrap
{
    /// <summary>
    /// Core class responsible for configuring web application environment via
    /// OWIN <c>IAppBuilder</c> interface.
    /// </summary>
    /// <remarks>
    /// Typically, aspects of configuration should be componentized into smaller
    /// partial classes, thereby separating the various configuration concerns
    /// (e.g. WebAPI routing, security, request filtering, etc.).
    /// </remarks>
    public partial class Startup
    {
        #region public methods

        public void Configuration(IAppBuilder app)
        {
            // initialize cors
            app.UseCors(CorsOptions.AllowAll);

            // initialize authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                AuthenticationMode = AuthenticationMode.Active,
                ExpireTimeSpan = TimeSpan.FromHours(1),
                SlidingExpiration = true                
            });

            // initialize webapi
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}/{id}",
                defaults: new 
                { 
                    id = RouteParameter.Optional 
                });

            // initialize dependency injection
            ConfigureDependencies(app, config);

            // bind web api
            app.UseWebApi(config);
        }

        #endregion

    }
}
