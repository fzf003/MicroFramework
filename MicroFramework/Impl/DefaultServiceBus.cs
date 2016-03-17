using ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework.Impl
{
    public class DefaultServiceBus : IServiceBus
    {
        private readonly ICommandBus _commandBus;
        private readonly IEventBus _eventBus;
        private readonly IQueryBus _queryBus;
        public DefaultServiceBus(ICommandBus commandbus,IEventBus eventbus,IQueryBus querybus)
        {
            this._eventBus = eventbus;
            this._commandBus = commandbus;
            this._queryBus = querybus;
       
        }

        public Task<CommandResult> SendAsync(ICommand busCommand)
        {
            if (busCommand == null) throw new ArgumentNullException(busCommand.GetType().Name);

            return this._commandBus.Dispatch(busCommand);
        }

        public async Task PublishAsync(IEvent busEvent)
        {
            if (busEvent == null) throw new ArgumentNullException(busEvent.GetType().Name);

            await Task.Run(() => this._eventBus.Publish(busEvent));
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> busQuery)
        {
            if (busQuery == null) throw new ArgumentNullException(busQuery.GetType().Name);
            return await this._queryBus.QueryAsync<TResult>(busQuery);
        }

        public void ToSubscribe<TEvent>(IObserver<TEvent> handler)
        {
             this._eventBus.ToMessage<TEvent>().Subscribe(handler);
        }


    }
}
