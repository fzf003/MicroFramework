using MicroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Command.Handler
{
    public class UserCommandHandler : ICommandHandler<AddUserCommand>,
                                      ICommandHandler<ChangeNameCommand>
    {
        public Task<object> ExecuteAsync(AddUserCommand command)
        {
            Console.WriteLine("添加用户");
            var user = new Domain.User() { Id=1, Age = command.Age, Name = command.Name };
            Repository.UserStore.Add(user);
            return Task.FromResult<object>(user);
        }

        public  Task<object> ExecuteAsync(ChangeNameCommand command)
        {
            var user = Repository.UserStore.FirstOrDefault(p => p.Id == command.Id);
            user.ChangeName(command.Name);
            //throw new Exception("ssd");
           return Task.FromResult<object>(user);
         }
    }
}
