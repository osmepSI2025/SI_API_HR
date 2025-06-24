using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Services
{
    public interface IJobService
    {
        Task<ApiListJobTitleResponse> GetAllJobTitles();
        Task<ApiJobTitleResponse> GetJobTitleById(string id);
        Task AddJobTitle(MJobTitle jobTitle);
        Task UpdateJobTitle(MJobTitle jobTitle);
        Task DeleteJobTitle(int id);
        Task<IEnumerable<MJobTitle>> SearchJobTitles(JobTitleModels searchModel);
        Task BatchEndOfDay();

    }
}
