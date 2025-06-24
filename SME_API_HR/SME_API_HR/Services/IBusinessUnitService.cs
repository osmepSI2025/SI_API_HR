using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Services
{
    public interface IBusinessUnitService
    {
        Task<BusinessUnitApiResponse> GetAllBusinessUnits();
        Task<BusinessUnitApiResponse> GetBusinessUnitById(string id);
        Task AddBusinessUnit(MBusinessUnit businessUnit);
        Task UpdateBusinessUnit(MBusinessUnit businessUnit);
        Task DeleteBusinessUnit(int id);
        Task<MBusinessUnit> GetByBu(string Buid);
        Task BatchEndOfDay();
        Task<IEnumerable<MBusinessUnit>> SearchBusinessUnits(MBusinessUnitModels searchModel);

    }
}
