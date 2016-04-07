using MicroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Query
{
    public class QueryOrderHandler : IQueryHandler<GetAll, List<Order.Application.Model.Order>>,
                                     IQueryHandler<QuerySingle,Order.Application.Model.Order>
    {
        public Task<List<Order.Application.Model.Order>> ExecuteQueryAsync(GetAll query)
        {
            return Task.FromResult(Repository.DB);
        }

        public Task<Model.Order> ExecuteQueryAsync(QuerySingle query)
        {
            return Task.FromResult(Repository.DB.FirstOrDefault(p => p.Id == query.AggregateId));
        }
    }
}
