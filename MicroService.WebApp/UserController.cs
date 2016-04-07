using MicroFramework;
using MicroFramework.Impl;
using MicroService.Application.Command;
using MicroService.Application.Domain;
using MicroService.Application.Query;
using ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroService.Web
{
    [RoutePrefix("api/user")]
   
    public class UserController : ApiController
    {
        private readonly IServiceBus servicebus;

        public UserController()
        {
             this.servicebus = ServiceLocator.Current.GetInstance<IServiceBus>();
        }

        public async Task<List<User>> Get()
        {
            return await this.servicebus.QueryAsync<List<User>>(new GetAll());
        }


        public string Get(int id)
        {
            return "value";
        }


        public async Task<CommandResult> Post([FromBody]string value)
        {
            return await this.servicebus.SendAsync(new AddUserCommand()
            {
                Age = 10,
                Name = Guid.NewGuid().ToString()
            });
        }

        
        public async Task<CommandResult> Put(int id, [FromBody]string value)
        {
            return await this.servicebus.SendAsync(new ChangeNameCommand()
            {
                Id = "1",

                Name = value
            });
     
        }


        public void Delete(int id)
        {

        }
    }
}
