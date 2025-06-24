using SME_API_HR.Models;

namespace SME_API_HR.Services
{
    public interface ICallAPIService
    {
        Task<ApiEmployeeResponse> GetDataEmpByEmpId(MapiInformationModels apiModels,string EmpId);
        Task<ApiListEmployeeResponse> GetDataApiAsync_EmpPloyee(MapiInformationModels apiModels, int page, int perPage);
        Task<ApiEmployeeProfileResponse> GetDataEmpProfileByEmpId(MapiInformationModels apiModels, string EmpId);
        Task<ApiListBusinessUnitResponse> GetDataApiAsync_Organization(MapiInformationModels apiModels);
        Task<ApiListPositionResponse> GetDataApiAsync_Position(MapiInformationModels apiModels);
        Task<ApiListEmployeeMovmentResponse> GetDataEmpMovementByEmpId(MapiInformationModels apiModels, string EmpId);
        Task<ApiTOrganizationTreeResponse> GetDataApiAsync_OrgTree(MapiInformationModels apiModels);
        Task<EmployeeContractApiResponse> GetDataApiAsync_EmployeeContract(MapiInformationModels apiModels, searchEmployeeContractModels Msearch);
    }
}
