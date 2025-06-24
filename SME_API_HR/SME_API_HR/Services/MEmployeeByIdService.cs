using SME_API_HR.Entities;
using SME_API_HR.Repository;
using SME_API_HR.Models;
namespace SME_API_HR.Services
{
    public class MEmployeeByIdService : IMEmployeeByIdService
    {
        private readonly IMEmployeeByIdRepository _repository;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly ICallAPIService _serviceApi;
        private readonly IMEmployeeByIdRepository _employeeRepository;
        public MEmployeeByIdService(IMEmployeeByIdRepository repository, IApiInformationRepository repositoryApi, ICallAPIService serviceApi
            , IMEmployeeByIdRepository employeeRepository)
        {
            _repository = repository;
            _repositoryApi = repositoryApi;
            _serviceApi = serviceApi;
            _employeeRepository = employeeRepository;

        }

        public async Task<IEnumerable<MEmployeeById>> GetAllEmployeesById()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MEmployeeById> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<ApiEmployeeResponse> GetEmployeeById(string EmpId)
        {
            var Lemp = new ApiEmployeeResponse();
            var employee = await GetAllEmployeesById(EmpId);
            if (employee.Results == null)
            {
                //call api to get employee details


                var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "employee-person" });
                if (!LApi.Any())
                {
                    return null;
                }


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
                }).First(); // ดึงตัวแรกของ List
                if (apiParam == null)
                {
                    return null;
                }
                var apiResponse = await _serviceApi.GetDataEmpByEmpId(apiParam, EmpId);
                if (apiResponse.Results == null)
                {
                    return null;
                }
                else
                {
                    MEmployeeById mEmployee = new MEmployeeById
                    {
                        EmployeeId = apiResponse.Results.EmployeeId,
                        BusinessUnitId = apiResponse.Results.BusinessUnitId,
                        CompanyId = apiResponse.Results.CompanyId,
                        Email = apiResponse.Results.Email,
                        EmployeeCode = apiResponse.Results.EmployeeCode,
                        EmployeeStatus = apiResponse.Results.EmployeeStatus,
                        EmployeeType = apiResponse.Results.EmployeeType,
                        FirstNameEn = apiResponse.Results.FirstNameEn,
                        FirstNameTh = apiResponse.Results.FirstNameTh,
                        LastNameEn = apiResponse.Results.LastNameEn,
                        LastNameTh = apiResponse.Results.LastNameTh,
                        Mobile = apiResponse.Results.Mobile,
                        NameEn = apiResponse.Results.NameEn,
                        NameTh = apiResponse.Results.NameTh,
                        PositionId = apiResponse.Results.PositionId,
                        SupervisorId = apiResponse.Results.SupervisorId,
                        EmploymentDate = apiResponse.Results.EmploymentDate,
                        TerminationDate = apiResponse.Results.TerminationDate
                    };
                    await AddEmployeeById(mEmployee);
                }

                // var Getemployee = await _employeeRepository.GetByIdAsync(EmpId);
                Lemp = new ApiEmployeeResponse
                {
                    Results = new EmployeeResult
                    {
                        EmployeeId = apiResponse.Results.EmployeeId,
                        BusinessUnitId = apiResponse.Results.BusinessUnitId,
                        CompanyId = apiResponse.Results.CompanyId,
                        Email = apiResponse.Results.Email,
                        EmployeeCode = apiResponse.Results.EmployeeCode,
                        EmployeeStatus = apiResponse.Results.EmployeeStatus,
                        EmployeeType = apiResponse.Results.EmployeeType,
                        FirstNameEn = apiResponse.Results.FirstNameEn,
                        FirstNameTh = apiResponse.Results.FirstNameTh,
                        LastNameEn = apiResponse.Results.LastNameEn,
                        LastNameTh = apiResponse.Results.LastNameTh,
                        Mobile = apiResponse.Results.Mobile,
                        NameEn = apiResponse.Results.NameEn,
                        NameTh = apiResponse.Results.NameTh,
                        PositionId = apiResponse.Results.PositionId,
                        SupervisorId = apiResponse.Results.SupervisorId,
                        EmploymentDate = apiResponse.Results.EmploymentDate,
                        TerminationDate = apiResponse.Results.TerminationDate
                    }
                };
                Lemp = await GetAllEmployeesById(EmpId);
                return Lemp;
            }
            else
            {
               
                return employee;
                
            }


        }

        public async Task AddEmployeeById(MEmployeeById employeeById)
        {
            await _repository.AddAsync(employeeById);
        }

        public async Task UpdateEmployeeById(MEmployeeById employeeById)
        {
            await _repository.UpdateAsync(employeeById);
        }

        public async Task DeleteEmployeeById(string id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task<ApiEmployeeResponse> GetAllEmployeesById(string EmpId)
        {
            var response = new ApiEmployeeResponse();
            try
            {
                var emp = await _employeeRepository.GetByIdAsync(EmpId);
                if (emp != null)
                {
                    response.Results = new EmployeeResult
                    {
                        EmployeeId = emp.EmployeeId,
                        EmployeeCode = emp.EmployeeCode,
                        NameTh = emp.NameTh,
                        NameEn = emp.NameEn,
                        FirstNameTh = emp.FirstNameTh,
                        FirstNameEn = emp.FirstNameEn,
                        LastNameTh = emp.LastNameTh,
                        LastNameEn = emp.LastNameEn,
                        Email = emp.Email,
                        Mobile = emp.Mobile,
                        EmploymentDate = emp.EmploymentDate,
                        TerminationDate = emp.TerminationDate,
                        EmployeeType = emp.EmployeeType,
                        EmployeeStatus = emp.EmployeeStatus,
                        SupervisorId = emp.SupervisorId,
                        CompanyId = emp.CompanyId,
                        BusinessUnitId = emp.BusinessUnitId,
                        PositionId = emp.PositionId
                    };
                }
                else
                {
                    response.Results = null;
                }
                return response;
            }
            catch (Exception)
            {
                response.Results = null;
                return response;
            }
        }
    }
}
