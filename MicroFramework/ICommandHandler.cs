using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework
{
    public interface ICommandHandler
    {

    }

    
    public interface ICommandHandler<TCommand> : ICommandHandler
         where TCommand : ICommand
    {
        Task<object> ExecuteAsync(TCommand command);
    }

}
