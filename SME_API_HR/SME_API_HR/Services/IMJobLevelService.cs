using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Services
{
    public interface IMJobLevelService
    {
        Task<ApiListJobLevelResponse> GetAllJobLevels();
        Task<ApiJobLevelResponse> GetJobLevelById(string id);
        Task AddJobLevel(MJobLevel jobLevel);
        Task UpdateJobLevel(MJobLevel jobLevel);
        Task DeleteJobLevel(int id);
        Task<IEnumerable<MJobLevel>> SearchJobLevel(MJobLevelModels searchModel);
        Task BatchEndOfDay();
    }
}
