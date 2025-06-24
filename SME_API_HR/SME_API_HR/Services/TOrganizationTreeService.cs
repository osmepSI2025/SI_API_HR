using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Repository;

namespace SME_API_HR.Services
{
    public class TOrganizationTreeService : ITOrganizationTreeService
    {
        private readonly ITOrganizationTreeRepository _repository;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly ICallAPIService _serviceApi;

        public TOrganizationTreeService(ITOrganizationTreeRepository repository, IApiInformationRepository repositoryApi, ICallAPIService serviceApi)
        {
            _repository = repository;
            _repositoryApi = repositoryApi;
            _serviceApi = serviceApi;
        }

        public async Task<IEnumerable<TOrganizationTree>> GetAllOrganizationTrees()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching all organization trees", ex);
            }
        }

        public async Task<TOrganizationTree> GetOrganizationTreeById(string id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching organization tree with ID {id}", ex);
            }
        }

        public async Task AddOrganizationTree(TOrganizationTree organizationTree)
        {
            try
            {
                await _repository.AddAsync(organizationTree);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding organization tree", ex);
            }
        }

        public async Task UpdateOrganizationTree(TOrganizationTree organizationTree)
        {
            try
            {
                await _repository.UpdateAsync(organizationTree);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating organization tree", ex);
            }
        }

        public async Task DeleteOrganizationTree(int id)
        {
            try
            {
                if (id == 0)
                {
                    // Log the action of deleting all records
                    Console.WriteLine("Deleting all records from T_Organization_Tree...");

                    // Call the repository method to delete all records
                    await _repository.DeleteAllAsync();

                    Console.WriteLine("All records deleted successfully.");
                }
                else
                {
                    // Log the action of deleting a specific record
                    Console.WriteLine($"Deleting organization tree with ID {id}...");

                    // Call the repository method to delete a specific record
                    await _repository.DeleteAsync(id);

                    Console.WriteLine($"Organization tree with ID {id} deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error occurred while deleting organization tree with ID {id}: {ex.Message}");

                // Rethrow the exception with additional context
                throw new Exception($"Error deleting organization tree with ID {id}", ex);
            }
        }

        public async Task BatchEndOfDay()
        {
            try
            {
                var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "organization-tree" });

                var apiParam = LApi.Select(x => new MapiInformationModels
                {
                    ServiceNameCode = x.ServiceNameCode,
                    ApiKey = x.ApiKey,
                    AuthorizationType = x.AuthorizationType,
                    ContentType = x.ContentType,
                    CreateDate = x.CreateDate,
                    Id = x.Id,
                    MethodType = x.MethodType,
                    ServiceNameTh = x.ServiceNameTh,
                    Urldevelopment = x.Urldevelopment,
                    Urlproduction = x.Urlproduction,
                    Username = x.Username,
                    Password = x.Password,
                    UpdateDate = x.UpdateDate
                }).First(); // ???????????? List

                var apiResponse = await _serviceApi.GetDataApiAsync_OrgTree(apiParam);
                if (apiResponse != null)
                {
                    // delete before save

                    await _repository.DeleteAllAsync();
                    await SaveBusinessUnitsAsync(apiResponse);
               
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SaveBusinessUnitsAsync(ApiTOrganizationTreeResponse units)
        {
            try
            {
                await _repository.SaveBusinessUnitsAsync(units);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving business units", ex);
            }
        }
        public async Task<ApiTOrganizationTreeResponse> GetOrganizationTreeHierarchy()
        {
            try
            {
                // Fetch all organization tree records
                var allNodes = await _repository.GetAllAsync();

                // Build the hierarchy starting from the root nodes (nodes with no parent)
                var rootNodes = allNodes
                    .Where(node => string.IsNullOrEmpty(node.ParentBusinessUnitId))
                    .Select(node => BuildHierarchy(node, allNodes))
                    .ToList();

                // Return the result in the desired format
                return new ApiTOrganizationTreeResponse
                {
                    Results = rootNodes
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching organization tree hierarchy", ex);
            }
        }

        private BusinessUnit BuildHierarchy(TOrganizationTree node, IEnumerable<TOrganizationTree> allNodes)
        {
            return new BusinessUnit
            {
                BusinessUnitId = node.BusinessUnitId,
                BusinessUnitNameTh = node.BusinessUnitNameTh,
                BusinessUnitNameEn = node.BusinessUnitNameEn,
                Children = allNodes
                    .Where(child => child.ParentBusinessUnitId == node.BusinessUnitId)
                    .Select(child => BuildHierarchy(child, allNodes))
                    .ToList()
            };
        }

    }
}
