using Microsoft.AspNetCore.Builder;
using UserRegistrationService.Middlewares;

namespace UserRegistrationService.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}