using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Filter
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            context.Result = new ContentResult
            {
                Content = "Xatolik yuz berdi, iltimos keyinroq urinib ko'ring.",
                ContentType = "text/plain",
                StatusCode = 500 
            };

       
            Console.WriteLine($"Xatolik: {exception.Message}");
        }
    }
    }
}
