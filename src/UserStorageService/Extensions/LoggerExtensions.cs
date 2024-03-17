using Microsoft.Extensions.Hosting;
using Serilog;

namespace UserStorageService.Extensions
{
    public static class LoggerExtensions
    {
        public static IHostBuilder ConfigureLogger(this IHostBuilder builder)
        {
            builder.UseSerilog((ctx, cfg) =>
            {
                cfg.Enrich.FromLogContext()
                    .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName).ReadFrom
                    .Configuration(ctx.Configuration);
            });

            return builder;
        }
    }
}