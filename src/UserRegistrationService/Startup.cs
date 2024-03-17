using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using UserRegistrationService.Application;
using UserRegistrationService.Extensions;
using UserRegistrationService.Infrastructure;

namespace UserRegistrationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwagger();

            services.AddApplicationLayer();
            services.AddInfrastructure(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureSwagger(env);
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseMiddlewares();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}