using SME_API_HR.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public interface ITEmployeeProfileRepository
    {
        Task<IEnumerable<TEmployeeProfile>> GetAllAsync();
        Task<TEmployeeProfile> GetByIdAsync(string EmpId);

        Task AddAsync(TEmployeeProfile profile);
        Task UpdateAsync(TEmployeeProfile profile);
        Task DeleteAsync(int id);
    }
}
