using Microsoft.AspNetCore.Builder;
using UserStorageService.Middlewares;

namespace UserStorageService.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}