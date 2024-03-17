using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserStorageService.Infrastructure.Data;

namespace UserStorageService.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("UsersDb"));
            });
        }
    }
}