using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
 
using System.Web.Http;

namespace MicroService.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://*:4242")) 
            {
                Console.WriteLine("API服务已启动...");
                Console.ReadKey();
            }
            
         }
    }

   
}
