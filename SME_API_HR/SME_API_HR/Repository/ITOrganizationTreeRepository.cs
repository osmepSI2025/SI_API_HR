using SME_API_HR.Entities;
using SME_API_HR.Models;

namespace SME_API_HR.Repository
{
    public interface ITOrganizationTreeRepository
    {
        Task<IEnumerable<TOrganizationTree>> GetAllAsync();
        Task<TOrganizationTree> GetByIdAsync(string id);
        Task AddAsync(TOrganizationTree organizationTree);
        Task UpdateAsync(TOrganizationTree organizationTree);
        Task DeleteAsync(int id);
        Task SaveBusinessUnitsAsync(ApiTOrganizationTreeResponse units);
        Task DeleteAllAsync();
    }
}
