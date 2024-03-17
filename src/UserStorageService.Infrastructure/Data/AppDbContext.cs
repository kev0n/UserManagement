using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserStorageService.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Organization>()
                .HasMany(x => x.Users)
                .WithOne(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId);

            SeedDefaultData(modelBuilder);
        }

        private static void SeedDefaultData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasData(SeedData.GetDefaultOrganizations());
        }
    }
}