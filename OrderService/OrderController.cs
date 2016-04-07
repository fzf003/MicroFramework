using MicroFramework;
using MicroFramework.Impl;
using Order.Application.Command;
using Order.Application.Query;
using ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrderService
{
     [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        private readonly IServiceBus servicebus;

        public OrderController()
        {
            Console.WriteLine("初始化");
            this.servicebus = ServiceLocator.Current.GetInstance<IServiceBus>();
            Console.WriteLine(this.servicebus==null);
        }

        public async Task<List<Order.Application.Model.Order>> Get()
        {
            Console.WriteLine("Query:");
            return await this.servicebus.QueryAsync<List<Order.Application.Model.Order>>(new GetAll());
        }

        public async Task<CommandResult> Post([FromBody]string value)
        {
            return await this.servicebus.SendAsync(new CreateOrderCommand()
            {
                CustomerId=Guid.NewGuid().ToString("N"),
                ProductId=Guid.NewGuid().ToString("N"),
                Quantity=5
            });
        }
    }
}
