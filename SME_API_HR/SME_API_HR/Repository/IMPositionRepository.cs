using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public interface IMPositionRepository
    {
        Task<IEnumerable<MPosition>> GetAllAsync();
        Task<MPosition> GetByIdAsync(string code);
        Task AddAsync(MPosition position);
        Task UpdateAsync(MPosition position);
        Task DeleteAsync(int id);
        Task<MPosition> GetByPostion(string PositionCode);
        Task<IEnumerable<MPosition>> SearchAsync(MPositionModels searchModel);
    }
}
