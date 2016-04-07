using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework.Impl
{
    public class DefaultCommandContext : ICommandContext
    {
        private readonly ICommand _command;
        private readonly ICommandHandler _commandhandler;

        public DefaultCommandContext(ICommand command, ICommandHandler commandhandler)
        {
            this._command = command;

            this._commandhandler = commandhandler;
         }

        public ICommandHandler CommandHandler
        {
            get
            {
                return this._commandhandler;
            }
        }

        public ICommand Command
        {
            get
            {
                return this._command;
            }
        }

    }
}
