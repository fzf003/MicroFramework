using MicroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Query
{
    public class GetAll : IQuery<List<Order.Application.Model.Order>>
    {

    }

    public class QuerySingle:IQuery<Order.Application.Model.Order>
    {
        public string AggregateId { get; set; }
    }
}
