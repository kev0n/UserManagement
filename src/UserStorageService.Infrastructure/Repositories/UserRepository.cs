using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserStorageService.Infrastructure.Data;

namespace UserStorageService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User?> FindByIdAsync(int id)
        {
            return await _dbContext.Users
                .Include(x => x.Organization)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _dbContext.Update(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<(User[] users, int totalCount)> GetPagedUsersByOrganizationId(int organizationId, IPagination pagination)
        {
            var query = _dbContext.Users.AsNoTracking().AsQueryable()
                .Where(x => x.OrganizationId == organizationId);

            var totalCount = await query.CountAsync();
            if (totalCount == 0)
                return (Array.Empty<User>(), totalCount);

            var items = await query
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToArrayAsync();

            return (items, totalCount);
        }
    }
}