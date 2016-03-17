using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework.Impl
{
    public class DefaultCommandContext : ICommandContext
    {

        public DefaultCommandContext(ICommand command, ICommandHandler commandhandler)
        {
            this.Command = command;
            this.CommandHandler = commandhandler;

        }

        public ICommandHandler CommandHandler
        {
            get;
            private set;
        }

        public ICommand Command
        {
            get;
            private set;
        }

    }
}
