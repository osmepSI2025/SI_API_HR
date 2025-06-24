using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Services
{
    public interface IEmployeeService
    {
        Task<ApiListEmployeeResponse> GetAllEmployees(searchEmployeeModels searchModel);
        Task<MEmployee> GetEmployeeById(string id);
        Task AddEmployee(MEmployee employee);
        Task UpdateEmployee(MEmployee employee);
        Task DeleteEmployee(int id);
        Task<BusinessUnitsEmployeeApiResponse?> GetEmployeeByOrganization(string? businessUnitId);
        Task GetEmployeeByBatchEndOfDay(searchEmployeeModels Models);
        Task<IEnumerable<MEmployee>> SearchEmployee(MEmployeeModels searchModel);
    }
}
