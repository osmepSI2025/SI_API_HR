using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Repository;

namespace SME_API_HR.Services
{
    public class TEmployeeContractService : ITEmployeeContractService
    {
        private readonly ITEmployeeContractRepository _repository;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly ICallAPIService _serviceApi;

        public TEmployeeContractService(ITEmployeeContractRepository repository

           , IApiInformationRepository repositoryApi, ICallAPIService serviceApi)
        {
            _repository = repository;
            _repositoryApi = repositoryApi;
            _serviceApi = serviceApi;
        }

        public async Task<IEnumerable<TEmployeeContract>> GetAllContracts()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TEmployeeContract?> GetContractById(string empployeeid, DateTime? xdate)
        {
            return await _repository.GetContractById(empployeeid, xdate);
        }

        public async Task AddContract(TEmployeeContract contract)
        {
            await _repository.AddAsync(contract);
        }

        public async Task UpdateContract(TEmployeeContract contract)
        {
            await _repository.UpdateAsync(contract);
        }

        public async Task DeleteContract(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task BatchEndOfDay(searchEmployeeContractModels models)
        {
            try
            {
                var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "employee-contract" });

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


                //for (int i = 1; i < 3; i++)
                //{

                try
                {
                    var apiResponse = await _serviceApi.GetDataApiAsync_EmployeeContract(apiParam, models);
                    if (apiResponse == null || apiResponse.Results == null)
                    {

                    }
                    else
                    {
                        foreach (var item in apiResponse.Results)
                        {

                            var EmpX = await GetContractById(item.EmployeeId.ToString(), item.EmploymentDate);
                            if (EmpX == null)
                            {
                                TEmployeeContract mEmployee = new TEmployeeContract
                                {
                                    ContractFlag = item.ContractFlag,
                                    EmployeeId = item.EmployeeId.ToString(),
                                    EmployeeCode = item.EmployeeCode,
                                    NameTh = item.NameTh,
                                    NameEn = item.NameEn,
                                    FirstNameEn = item.FirstNameEn,
                                    FirstNameTh = item.FirstNameTh,
                                    LastNameEn = item.LastNameEn,
                                    LastNameTh = item.LastNameTh,
                                    Email = item.Email,
                                    Mobile = item.Mobile,
                                    EmploymentDate = item.EmploymentDate,
                                    TerminationDate = item.TerminationDate,
                                    EmployeeType = item.EmployeeType,
                                    EmployeeStatus = item.EmployeeStatus,
                                    SupervisorId = item.SupervisorId,
                                    CompanyId = item.CompanyId,
                                    BusinessUnitId = item.BusinessUnitId,
                                    PositionId = item.PositionId,
                                    Salary = item.Salary,
                                    IdCard = item.IdCard,

                                };

                                await AddContract(mEmployee);
                            }
                            else if (string.IsNullOrEmpty(EmpX.PositionId))
                            {
                                // update data by emp
                                EmpX.EmployeeId = item.EmployeeId;
                                EmpX.PositionId = item.PositionId;

                                // update data
                                await UpdateContract(EmpX);
                            }


                            //var employeeProfile = await _employeeRepository.GetEmployeeProfileById(item.EmployeeId.ToString());


                        }

                        //var newpagemodels = new searchEmployeeContractModels
                        //{
                        //    employmentDate = models.employmentDate,
                        //    page = models.page + 1,
                        //    perPage = models.perPage,

                        //};
                        //if (apiResponse.Results.Count != 0)
                        //{
                        //    await BatchEndOfDay(newpagemodels);
                        //}

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                //}

            }
            catch (Exception ex)
            {

            }
        }


        public async Task<EmployeeContractApiResponse> SearchEmployeeContract(searchEmployeeContractModels models)
        {
            try
            {
                var resultContact = await _repository.SearchEmployeeContract(models);
                var response = new EmployeeContractApiResponse
                {
                    Results = resultContact?.Select(item => new EmployeeContractResult
                    {
                        ContractFlag = item.ContractFlag,
                        EmployeeId = item.EmployeeId,
                        EmployeeCode = item.EmployeeCode,
                        NameTh = item.NameTh,
                        NameEn = item.NameEn,
                        FirstNameEn = item.FirstNameEn,
                        FirstNameTh = item.FirstNameTh,
                        LastNameEn = item.LastNameEn,
                        LastNameTh = item.LastNameTh,
                        Email = item.Email,
                        Mobile = item.Mobile,
                        EmploymentDate = item.EmploymentDate,
                        TerminationDate = item.TerminationDate,
                        EmployeeType = item.EmployeeType,
                        EmployeeStatus = item.EmployeeStatus,
                        SupervisorId = item.SupervisorId,
                        CompanyId = item.CompanyId,
                        BusinessUnitId = item.BusinessUnitId,
                        PositionId = item.PositionId,
                        Salary = item.Salary,
                        IdCard = item.IdCard,
                        PassportNo = item.PassportNo
                    }).ToList() ?? new List<EmployeeContractResult>()
                };

                response.Pagination = new Pagination
                {
                    TotalRecords = models.perPage,
                    CurrentPage = models.page,
                    PrevPage = models.page - 1 == 0 ? null : models.page - 1,
                    PageCount = 1
                };

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search EmployeeContract: {ex.Message}");
                return new EmployeeContractApiResponse { Results = new List<EmployeeContractResult>() };
            }
        }


        public async Task<ApiEmployeeContractResponse> SearchEmployeeContractByEmpId(searchEmployeeContractModels models)
        {
            try
            {
                var resultContact = await _repository.GetContractById(models.EmployeeId, models.employmentDate);

                if (resultContact == null)
                    return null;

                var responseData = new ApiEmployeeContractResponse
                {
                    Results = new EmployeeContractResult
                    {
                        ContractFlag = resultContact.ContractFlag,
                        EmployeeId = resultContact.EmployeeId,
                        EmployeeCode = resultContact.EmployeeCode,
                        NameTh = resultContact.NameTh,
                        NameEn = resultContact.NameEn,
                        FirstNameEn = resultContact.FirstNameEn,
                        FirstNameTh = resultContact.FirstNameTh,
                        LastNameEn = resultContact.LastNameEn,
                        LastNameTh = resultContact.LastNameTh,
                        Email = resultContact.Email,
                        Mobile = resultContact.Mobile,
                        EmploymentDate = resultContact.EmploymentDate,
                        TerminationDate = resultContact.TerminationDate,
                        EmployeeType = resultContact.EmployeeType,
                        EmployeeStatus = resultContact.EmployeeStatus,
                        SupervisorId = resultContact.SupervisorId,
                        CompanyId = resultContact.CompanyId,
                        BusinessUnitId = resultContact.BusinessUnitId,
                        PositionId = resultContact.PositionId,
                        Salary = resultContact.Salary,
                        IdCard = resultContact.IdCard,
                        PassportNo = resultContact.PassportNo
                    }
                };

                return responseData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search EmployeeContract: {ex.Message}");
                return null;
            }
        }

    }
}
