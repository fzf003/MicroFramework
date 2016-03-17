using MicroFramework.Impl;
using System;
namespace MicroFramework
{
    public interface ICommandBus
    {
        System.Threading.Tasks.Task<CommandResult> Dispatch(MicroFramework.ICommand command);
    }
}
