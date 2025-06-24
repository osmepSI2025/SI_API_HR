using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public interface IMJobLevelRepository
    {
        Task<IEnumerable<MJobLevel>> GetAllAsync();
        Task<MJobLevel> GetByIdAsync(string id);
        Task AddAsync(MJobLevel jobLevel);
        Task UpdateAsync(MJobLevel jobLevel);
        Task DeleteAsync(int id);
        Task<IEnumerable<MJobLevel>> SearchAsync(MJobLevelModels searchModel);
        Task<MJobLevel> GetByJobLevel(string code);
    }
}
