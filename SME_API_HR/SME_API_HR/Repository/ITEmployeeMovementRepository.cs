using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public interface ITEmployeeMovementRepository
    {
        Task<IEnumerable<TEmployeeMovement>> GetAllAsync();
        Task<TEmployeeMovement> GetByIdAsync(string employeeId);
        Task AddAsync(TEmployeeMovement movement);
        Task UpdateAsync(TEmployeeMovement movement);
        Task DeleteAsync(string employeeId);
        Task<IEnumerable<TEmployeeMovement>> SearchAsync(TEmployeeMovementModels searchModel);

        Task<TEmployeeMovement> GetTEmployeeMovementsById(string id);
       
    }
}
