using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eCommerce_API.Filters
{
    public class UserAccessApiKey : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = config.GetValue<string>("UserApiKey");

            if (!context.HttpContext.Request.Query.TryGetValue("usercode", out var usercode))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!apiKey.Equals(usercode))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
