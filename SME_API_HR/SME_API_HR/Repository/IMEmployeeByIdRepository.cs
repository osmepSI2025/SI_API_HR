using SME_API_HR.Entities;

namespace SME_API_HR.Repository
{
    public interface IMEmployeeByIdRepository
    {
        Task<IEnumerable<MEmployeeById>> GetAllAsync();
        Task<MEmployeeById> GetByIdAsync(string id);
        Task AddAsync(MEmployeeById employeeById);
        Task UpdateAsync(MEmployeeById employeeById);
        Task DeleteAsync(string id);
    }
}
