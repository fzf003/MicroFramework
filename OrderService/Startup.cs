using MicroFramework;
using MicroFramework.Impl;
using Order.Application.Command;
using Order.Application.CommandHandler;
using Owin;
using ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TinyIoC;

namespace OrderService
{
    class Startup
    {

        public void Configuration(IAppBuilder appBuilder)
        {

            HttpConfiguration config = new HttpConfiguration();


            config.MapHttpAttributeRoutes();
            //config.Filters.Add(new DefaultErrorFilterAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            Bootstrapper.Startup();
            appBuilder.UseWebApi(config);
           

        }

        

         


    }
}
