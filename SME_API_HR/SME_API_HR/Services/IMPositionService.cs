using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Services
{
    public interface IMPositionService
    {
        Task<ApiListPositionResponse> GetAllPositions();
        Task<ApiPositionResponse> GetPositionById(string id);
        Task AddPosition(MPosition position);
        Task UpdatePosition(MPosition position);
        Task DeletePosition(int id);
        Task BatchEndOfDay();
        Task<IEnumerable<MPosition>> SearchPosition(MPositionModels searchModel);
    }
}
