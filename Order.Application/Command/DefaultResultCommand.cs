using MicroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Command
{
    public class DefaultResultCommand : ICommandResult
    {
        public static ICommandResult Create()
        {
            return new DefaultResultCommand(true);
        }

        public DefaultResultCommand(bool success)
        {
            this.Success = success;
        }

        public bool Success { get; protected set; }
    }
}
