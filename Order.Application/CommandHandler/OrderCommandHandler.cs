using MicroFramework;
using Order.Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.CommandHandler
{
    public class OrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        private IServiceBus _bus;
        public OrderCommandHandler(IServiceBus bus)
        {
            this._bus = bus;
        }

        public Task<ICommandResult> ExecuteAsync(CreateOrderCommand command)
        {
            Repository.DB.Add(new Model.Order() { CustomerId = command.CustomerId, ProductId = command.ProductId, Quantity = command.Quantity, Address = new Model.Address() { Zip="10000", City="北京", State="On", Street1="横一条" } });
            return Task.FromResult(DefaultResultCommand.Create());
        }
    }
}
