using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Swashbuckle.Application;
using Yara.IoC;
using Yara.WebApi.Controllers;
using Yara.WebApi.Security;

#pragma warning disable 1591

namespace Yara.WebApi
{
    public static class Startup
    {
        public static Container container;

        public static void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always // Add this line to enable detail mode in release
            };

            var path = $@"{AppDomain.CurrentDomain.BaseDirectory}bin\Yara.WebApi.xml";

            config.EnableSwagger(x =>
                {
                    x.SingleApiVersion("v1", "Yara - Portal Financeiro");
                    x.RootUrl(req => new Uri(req.RequestUri, HttpContext.Current.Request.ApplicationPath ?? string.Empty)
                        .ToString());


                    ;
                    x.IncludeXmlComments(path);
                }
            ).EnableSwaggerUi();
          
            config.Filters.Add(new UsuarioLogadoFilter());
            log4net.Config.XmlConfigurator.Configure();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/jpg"));
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            config.MessageHandlers.Insert(0, new ServerCompressionHandler(
            new GZipCompressor(),
            new DeflateCompressor()));

            config.Formatters.JsonFormatter
                .SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            config.MapHttpAttributeRoutes(); //This has to be called before the following OData mapping, so also before WebApi mapping

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            container = SimpleInjectorContainer.RegisterServices();

          

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
         
            Mappings.AutoMapperConfiguration.Initialize();
            GlobalConfiguration.Configuration.EnsureInitialized(); 
            ConfigureAuth(app);
            //ConfigureAuthSSOSalesForce(app);

            app.UseWebApi(config);
        }

        private static void ConfigureAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/v1/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new YaraAuthorizationServerProvider(),
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}