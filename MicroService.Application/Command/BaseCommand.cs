using MicroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Command
{
    public abstract class BaseCommand<TEntity> : ICommand<Guid, TEntity>
         where TEntity : IAggregateRoot
    {
        public Guid CommandId
        {
            get;
            set;
        }

        public Type AggregateRoot { get { return typeof(TEntity); } }

        public override string ToString()
        {
            return this.CommandId.ToString("N");
        }

       
    }

}
