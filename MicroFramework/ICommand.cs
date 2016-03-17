using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework
{
    public interface ICommand
    {

    }

    public interface ICommand<TId, TEntity> : ICommand
     where TEntity : IAggregateRoot
    {
        TId CommandId { get; set; }
    }

}
