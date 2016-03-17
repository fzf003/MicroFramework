using MicroFramework;
using MicroService.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Application.Query
{


    public class QueryUserHandler : IQueryHandler<GetAll, List<User>>
    {
        public Task<List<User>> ExecuteQueryAsync(GetAll query)
        {

            return Task.FromResult(Repository.UserStore);
        }

         
    }
}
