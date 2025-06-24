using SME_API_HR.Entities;
using SME_API_HR.Models;

namespace SME_API_HR.Repository
{
    public interface ITEmployeeContractRepository
    {
        Task<IEnumerable<TEmployeeContract>> GetAllAsync();
        Task<TEmployeeContract?> GetContractById(string employeeid,DateTime? xdate);
        Task AddAsync(TEmployeeContract contract);
        Task UpdateAsync(TEmployeeContract contract);
        Task DeleteAsync(int id);
        Task<IEnumerable<TEmployeeContract>> SearchEmployeeContract(searchEmployeeContractModels searchModel);
    }
}
