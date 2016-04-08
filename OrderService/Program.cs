using MicroFramework;
using Microsoft.Owin.Hosting;
using Order.Application.Command;
using Order.Application.Query;
using ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyMessenger;

namespace OrderService
{
    
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start("http://127.0.0.1:12345"))
            {
               
                Console.WriteLine("API服务已启动...");
                Console.ReadKey();
            }

            
           
        }
    }
}
