using MicroService.Web.Adapter;
using Owin;
using ServiceLocation;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroService.Web
{
    class Startup
    {

        public void Configuration(IAppBuilder appBuilder)
        {

            HttpConfiguration config = new HttpConfiguration();


            config.MapHttpAttributeRoutes();
            config.Filters.Add(new DefaultExceptionFilterAttribute());
            config.MessageHandlers.Add(new MessageHandler());
        
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            appBuilder.UseWebApi(config);
            appBuilder.Run((context) => {
                context.Response.WriteAsync("sdsdsd");
                return Task.FromResult(""); });
            appBuilder.UseSpringNet();

        }

         
    }

    public static class SpringNetExt
    {
        public static IAppBuilder UseSpringNet(this IAppBuilder appBuilder)
        {
            ServiceLocator.SetLocatorProvider(() => new SpringServiceLocatorAdapter(ContextRegistry.GetContext()));
            return appBuilder;
        }
    }
}
