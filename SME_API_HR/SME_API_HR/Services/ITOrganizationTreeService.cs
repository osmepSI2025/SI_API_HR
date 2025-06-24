using Microsoft.AspNetCore.Mvc;
using SME_API_HR.Entities;
using SME_API_HR.Models;

namespace SME_API_HR.Services
{
    public interface ITOrganizationTreeService
    {
        Task<IEnumerable<TOrganizationTree>> GetAllOrganizationTrees();
        Task<TOrganizationTree> GetOrganizationTreeById(string id);
        Task AddOrganizationTree(TOrganizationTree organizationTree);
        Task UpdateOrganizationTree(TOrganizationTree organizationTree);
        Task DeleteOrganizationTree(int id);
        Task BatchEndOfDay();
        Task SaveBusinessUnitsAsync(ApiTOrganizationTreeResponse units);
        Task<ApiTOrganizationTreeResponse> GetOrganizationTreeHierarchy();

    }
}
