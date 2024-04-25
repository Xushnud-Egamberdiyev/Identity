using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Filter
{
    public class ResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("DateTime", DateTime.Now.ToString());
        }
    }
}
