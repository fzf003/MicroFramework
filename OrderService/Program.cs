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
    public class TestMessage:TinyMessageBase
    {
        public TestMessage() : base(null) { }
        public string Name { get; set; }

      
    }
    class Program
    {
        static void Main(string[] args)
        {
            //using (WebApp.Start("http://127.0.0.1:12345"))
            //{
            //   new TinyMessengerHub()
            //    Console.WriteLine("API服务已启动...");
            //    Console.ReadKey();
            //}

            var hub= new TinyMessengerHub();
            hub.Subscribe<TestMessage>(p => { Console.WriteLine(p.Name); });


            hub.PublishAsync<TestMessage>(new TestMessage() { Name="dssd" });
            Console.ReadKey();
        }
    }
}
