using MicroFramework.Impl;
using System;
namespace MicroFramework
{
    public interface IServiceBus
    {
        System.Threading.Tasks.Task PublishAsync(MicroFramework.IEvent busEvent);
        System.Threading.Tasks.Task<TResult> QueryAsync<TResult>(MicroFramework.IQuery<TResult> busQuery);
        System.Threading.Tasks.Task<CommandResult> SendAsync(MicroFramework.ICommand busCommand);
        void ToSubscribe<TEvent>(IObserver<TEvent> handler);
    }
}
