using ServiceLocation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework.Impl
{
    public class DefaultQueryBus : IQueryBus
    {
        private class QueryHandlerItem
        {
            public Type QueryHandlerType { get; set; }
            public Func<object, object, object> HandlerFunc { get; set; }
        }


        readonly ConcurrentDictionary<Type, QueryHandlerItem> _cacheItems = new ConcurrentDictionary<Type, QueryHandlerItem>();

        public DefaultQueryBus()
        {

        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();

            var cacheItem = _cacheItems.GetOrAdd(
                queryType,
                CreateQueryHandler);

            var queryHandler = ServiceLocator.Current.GetInstance(cacheItem.QueryHandlerType);

            var task = (Task<TResult>)cacheItem.HandlerFunc(queryHandler, query);

            return await task.ConfigureAwait(false);
        }

        static QueryHandlerItem CreateQueryHandler(Type queryType)
        {
            var queryInterfaceType = queryType
                .GetInterfaces()
                .Single(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuery<>));
            var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, queryInterfaceType.GetGenericArguments()[0]);
            var methodInfo = queryHandlerType.GetMethod("ExecuteQueryAsync");
            return new QueryHandlerItem
            {
                QueryHandlerType = queryHandlerType,
                HandlerFunc = (h, q) => methodInfo.Invoke(h, new object[] { q })
            };
        }
    }
}
