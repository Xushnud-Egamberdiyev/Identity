using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Identity.Filter
{
    public class Permission : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }

            var userRole = context.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if(userRole == "user")
            {
                context.Result = new BadRequestResult();
                return;
            }
        }

        qoganiga vaqt yetmay qoldi ertalab darsgacha tugatib qoyaman
    }
}
