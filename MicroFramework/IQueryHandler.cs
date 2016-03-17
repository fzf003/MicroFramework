using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework
{
    public interface IQueryHandler
    {

    }

    public interface IQueryHandler<in TQuery, TResult> : IQueryHandler
        where TQuery : IQuery<TResult>
    {
        Task<TResult> ExecuteQueryAsync(TQuery query);
    }
   
}
