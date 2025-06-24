using SME_API_HR.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Services
{
    public interface ITEmployeeProfileService
    {
        Task<IEnumerable<TEmployeeProfile>> GetAllProfiles();
        Task<ApiEmployeeProfileResponse> GetProfileById(string EmpId);
        Task AddProfile(TEmployeeProfile profile);
        Task UpdateProfile(TEmployeeProfile profile);
        Task DeleteProfile(int id);
    }
}
