using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Raven.Client;

namespace WebApi.App_Start
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ContextInitializeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var method = actionExecutedContext.Request.Method;
            if (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete)
            {
                var scope = actionExecutedContext.Request.GetDependencyScope();
                var session = (IDocumentSession)scope.GetService(typeof(IDocumentSession));
                session.SaveChanges();
            }
        }
    }
}