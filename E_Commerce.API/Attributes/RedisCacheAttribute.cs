using E_Commerce.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API.Attributes
{
    public class RedisCacheAttribute(int timeInSec) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            var value = await cacheService.GetAsync(cacheKey);

            if (string.IsNullOrEmpty(value))
            {
                context.Result = new ContentResult()
                {
                    Content = value,
                    StatusCode = 200,
                    ContentType = "application/json"

                };
                return;
            }
            else
            {
               var executed = await next.Invoke();
                if(executed.Result is OkObjectResult okResult)
                {
                    await cacheService.SetAsync(cacheKey, okResult.Value,TimeSpan.FromSeconds(timeInSec));
                }

            }
            return;

        }

        private static string CreateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);

            foreach (var item in request.Query)
            {
                key.Append($"{item.Key} | {item.Value}");
            }

            return key.ToString();
        }
    }
}
