using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Filter
{
    public class ResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("X-Custom-Header", "MyCustomValue");
        }
    }
}
