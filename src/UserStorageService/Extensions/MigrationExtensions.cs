using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserStorageService.Infrastructure.Data;

namespace UserStorageService.Extensions
{
    public static class MigrationExtensions
    {
        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}