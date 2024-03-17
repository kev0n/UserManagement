using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserStorageService.Infrastructure.Data;

namespace UserStorageService.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly AppDbContext _dbContext;

        public OrganizationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Organization?> FindByIdAsync(int id)
        {
            return await _dbContext.Organizations
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _dbContext.Organizations
                .AnyAsync(x => x.Id == id);
        }
    }
}