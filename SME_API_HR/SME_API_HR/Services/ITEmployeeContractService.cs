using SME_API_HR.Entities;
using SME_API_HR.Models;

namespace SME_API_HR.Services
{
    public interface ITEmployeeContractService
    {
        Task<IEnumerable<TEmployeeContract>> GetAllContracts();
        Task<TEmployeeContract?> GetContractById(string id,DateTime? xdate);
        Task AddContract(TEmployeeContract contract);
        Task UpdateContract(TEmployeeContract contract);
        Task DeleteContract(int id);
        Task<EmployeeContractApiResponse> SearchEmployeeContract(searchEmployeeContractModels searchModel);
        Task<ApiEmployeeContractResponse> SearchEmployeeContractByEmpId(searchEmployeeContractModels models);
        Task BatchEndOfDay(searchEmployeeContractModels models);
    }
}
