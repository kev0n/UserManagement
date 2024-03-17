using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);

        Task<User?> FindByIdAsync(int id);

        Task<bool> UpdateAsync(User user);

        Task<(User[] users, int totalCount)> GetPagedUsersByOrganizationId(int organizationId, IPagination pagination);
    }
}