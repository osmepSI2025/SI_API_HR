using SME_API_HR.Entities;
using SME_API_HR.Models;

namespace SME_API_HR.Services
{
    public interface IMEmployeeByIdService
    {
        Task<IEnumerable<MEmployeeById>> GetAllEmployeesById();
        Task<ApiEmployeeResponse> GetEmployeeById(string id);
        Task AddEmployeeById(MEmployeeById employeeById);
        Task UpdateEmployeeById(MEmployeeById employeeById);
        Task DeleteEmployeeById(string id);
    }
}
