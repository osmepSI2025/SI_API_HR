using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public interface IMBusinessUnitRepository
    {
        Task<IEnumerable<MBusinessUnit>> GetAllAsync();
        Task<MBusinessUnit> GetByIdAsync(string id);
        Task AddAsync(MBusinessUnit businessUnit);
        Task UpdateAsync(MBusinessUnit businessUnit);
        Task DeleteAsync(int id);
        Task<MBusinessUnit> GetByBu(string Buid);
        Task<IEnumerable<MBusinessUnit>> SearchAsync(MBusinessUnitModels searchModel);

    }
}
