using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

using System.Net.Http;

namespace OrderService
{
    public class DefaultErrorFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is Exception)
            {
                context.Response = context.Request.CreateResponse<dynamic>(HttpStatusCode.BadRequest, new
                {
                    message = "内部错误",
                    status = false,
                    code = 400
                });


            }
        }
    }
}
