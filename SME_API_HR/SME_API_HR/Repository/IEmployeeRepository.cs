using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<MEmployee>> GetAllAsync(searchEmployeeModels searchModel);
        Task<MEmployee> GetByIdAsync(string id);
        Task AddAsync(MEmployee employee);
        Task UpdateAsync(MEmployee employee);
        Task DeleteAsync(int id);
        Task<List<MEmployee>?> GetEmployeeByOrganization(string? businessUnitId);
      
        Task<IEnumerable<MEmployee>> SearchEmployee(MEmployeeModels searchModel);

    }
}
