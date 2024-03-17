using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace UserRegistrationService.Extensions
{
    public static class SwaggerExtensions
    {
        public static void ConfigureSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "User Registration Service API";
                    options.DisplayRequestDuration();
                    options.DocExpansion(DocExpansion.List);
                });
            }
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "User Registration Service API"
                });
            });
        }
    }
}