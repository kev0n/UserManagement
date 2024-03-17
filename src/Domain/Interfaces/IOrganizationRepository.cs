using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<Organization?> FindByIdAsync(int id);

        Task<bool> ExistAsync(int id);
    }
}