using MicroFramework;
using MicroFramework.Impl;
using Order.Application.Command;
using Order.Application.CommandHandler;
using Order.Application.Query;
using ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;

namespace OrderService
{
    public static class Bootstrapper
    {
        public static void Startup()
        {
            var continer = TinyIoCContainer.Current;
            continer.Register<IEventBus, DefaultEventBus>();
            continer.Register<ICommandBus, DefaultCommandBus>();
            continer.Register<IQueryBus, DefaultQueryBus>();
            continer.Register<IServiceBus, DefaultServiceBus>();
            continer.Register<IQueryHandler<GetAll, List<Order.Application.Model.Order>>, QueryOrderHandler>();
            continer.Register<IQueryHandler<QuerySingle, Order.Application.Model.Order>, QueryOrderHandler>();
            continer.Register<ICommandHandler<CreateOrderCommand>>((c, p) => new OrderCommandHandler(c.Resolve<IServiceBus>()));
            ServiceLocator.SetLocatorProvider(() => new TinyIoCServiceLocator());
        }
    }
}
