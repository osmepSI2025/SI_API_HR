using SME_API_HR.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public interface IJobTitleRepository
    {
        Task<IEnumerable<MJobTitle>> GetAllAsync();
        Task<MJobTitle> GetByIdAsync(string id);
        Task AddAsync(MJobTitle jobTitle);
        Task UpdateAsync(MJobTitle jobTitle);
        Task DeleteAsync(int id);
        Task<IEnumerable<MJobTitle>> SearchAsync(JobTitleModels searchModel);
        Task<MJobTitle> GetByJobTitle(string code);
    }
}
