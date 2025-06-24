using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Services
{
    public interface ITEmployeeMovementService
    {
        Task<IEnumerable<TEmployeeMovement>> GetAllMovements();
        Task<ApiListEmployeeMovementResponse> GetMovementById(string employeeId);
        Task AddMovement(TEmployeeMovement movement);
        Task UpdateMovement(TEmployeeMovement movement);
        Task DeleteMovement(string employeeId);
        Task<IEnumerable<TEmployeeMovement>> SearchMovements(TEmployeeMovementModels searchModel);
      //  Task BatchEmployeeMoveMentEndOfDay();
    
        Task UpsertEmployeeMovement(string employeeId);
    }
}
