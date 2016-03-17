using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace MicroService.Web
{
    public class DefaultExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is Exception)
            {
                context.Response = context.Request.CreateResponse<dynamic>(HttpStatusCode.BadRequest, new {

                    message = "内部错误",
                    status = false,
                    code = 400
                });
                 
               
            }
        }
    }

    public class MessageHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (true)
            {
                //var response = new HttpResponseMessage(HttpStatusCode.OK)
                //{
                //    Content = new StringContent("Hello!")
                //};
              

                //var tsc = new TaskCompletionSource<HttpResponseMessage>();
                //tsc.SetResult(response);
                return await base.SendAsync(request, cancellationToken);
            }
            
        }

       
    }
}
