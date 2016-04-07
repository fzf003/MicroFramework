using MicroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Command.Handler
{
    public class CommandResult:ICommandResult
    {
        public string Name { get { return Guid.NewGuid().ToString(); } }
    }
    public class UserCommandHandler : ICommandHandler<AddUserCommand>,
                                      ICommandHandler<ChangeNameCommand>
    {
        private IServiceBus _bus;
        public UserCommandHandler(IServiceBus bus)
        {
            this._bus = bus;
        }
        public Task<ICommandResult> ExecuteAsync(AddUserCommand command)
        {
            var user = new Domain.User("1") {  Age = command.Age, Name = command.Name };
            Repository.UserStore.Add(user);
            PublishEvent(user, _bus);
            return Task.FromResult<ICommandResult>(new CommandResult());
        }
        static void PublishEvent(IAggregateRoot root,IServiceBus bus)
        {
            bus.PublishAsync(root.GetUncommittedEvents());
            root.AcceptUncommittedEvents();
         }

        public Task<ICommandResult> ExecuteAsync(ChangeNameCommand command)
        {
            var user = Repository.UserStore.FirstOrDefault(p => p.Id == command.Id);
            user.ChangeName(command.Name);
            PublishEvent(user, _bus);
            return Task.FromResult<ICommandResult>(new CommandResult());
         }
    }
}
