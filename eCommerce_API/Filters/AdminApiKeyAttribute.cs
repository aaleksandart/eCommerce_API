using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eCommerce_API.Filters
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = config.GetValue<string>("AdminApiKey");

            if (!context.HttpContext.Request.Query.TryGetValue("admincode", out var admincode))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if(!apiKey.Equals(admincode))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
