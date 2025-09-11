using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Repository;

namespace SME_API_HR.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITEmployeeProfileService _profileService;
        private readonly ITEmployeeMovementService _movementService;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly ICallAPIService _serviceApi;
        private readonly IMEmployeeByIdService _memployeeByIdService;
        public EmployeeService(IEmployeeRepository employeeRepository, IApiInformationRepository repositoryApi, ICallAPIService serviceApi, ITEmployeeProfileService profileService,
            ITEmployeeMovementService movementService,IMEmployeeByIdService memployeeByIdService)
        {
            _employeeRepository = employeeRepository;
            _repositoryApi = repositoryApi;
            _serviceApi = serviceApi;
            _profileService = profileService;
            _movementService = movementService;
            _memployeeByIdService = memployeeByIdService;
        }

        public async Task<ApiListEmployeeResponse> GetAllEmployees(searchEmployeeModels smodel)
        {
            var response = new ApiListEmployeeResponse();
            var resPage = new Pagination();
            resPage.TotalRecords = smodel.perPage;
            resPage.CurrentPage = smodel.page;
           
            try
            {
                var employees = await _employeeRepository.GetAllAsync(smodel);
                if (employees != null && employees.Any())
                {
                    resPage.PrevPage = smodel.page - 1==0? null : smodel.page - 1;
                    resPage.PageCount = 1;
                    response.Pagination = resPage;
                    response.Results = employees.Select(emp => new EmployeeResult
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
                        PositionId = emp.PositionId,
                        PositionNameEn = emp.PositionNameEn,
                        PositionNameTh = emp.PositionNameTh,
                        JobLevelId = emp.JobLevelId,
                        JobLevelNameEn = emp.JobLevelNameEn,
                        JobLevelNameTh = emp.JobLevelNameTh,
                    }).ToList();
                }
                else
                {
                    response.Results = new List<EmployeeResult?>();
                }
                return response;
            }
            catch (Exception)
            {
                response.Results = new List<EmployeeResult?>();
                return response;
            }
        }


        public async Task<MEmployee> GetEmployeeById(string EmpId)
        {
            var employee = await _employeeRepository.GetByIdAsync(EmpId);
            return employee;
        }
      
        //public async Task<MEmployee> GetEmployeeById(string EmpId)
        //{
        //    var employee = await _employeeRepository.GetByIdAsync(EmpId);
        //    if (employee == null)
        //    {
        //        //call api to get employee details


        //        var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "employee-person" });
        //        if (!LApi.Any())
        //        {
        //            return null;
        //        }


        //        var apiParam = LApi.Select(x => new MapiInformationModels
        //        {
        //            ServiceNameCode = x.ServiceNameCode,
        //            ApiKey = x.ApiKey,
        //            AuthorizationType = x.AuthorizationType,
        //            ContentType = x.ContentType,
        //            CreateDate = x.CreateDate,
        //            Id = x.Id,
        //            MethodType = x.MethodType,
        //            ServiceNameTh = x.ServiceNameTh,
        //            Urldevelopment = x.Urldevelopment,
        //            Urlproduction = x.Urlproduction,
        //            Username = x.Username,
        //            Password = x.Password,
        //            UpdateDate = x.UpdateDate
        //        }).First(); // ดึงตัวแรกของ List
        //        if (apiParam == null)
        //        {
        //            return null;
        //        }
        //        var apiResponse = await _serviceApi.GetDataEmpByEmpId(apiParam, EmpId);
        //        if (apiResponse == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            MEmployee mEmployee = new MEmployee
        //            {
        //                EmployeeId = apiResponse.Results.EmployeeId,
        //                BusinessUnitId = apiResponse.Results.BusinessUnitId,
        //                CompanyId = apiResponse.Results.CompanyId,
        //                Email = apiResponse.Results.Email,
        //                EmployeeCode = apiResponse.Results.EmployeeCode,
        //                EmployeeStatus = apiResponse.Results.EmployeeStatus,
        //                EmployeeType = apiResponse.Results.EmployeeType,
        //                FirstNameEn = apiResponse.Results.FirstNameEn,
        //                FirstNameTh = apiResponse.Results.FirstNameTh,
        //                LastNameEn = apiResponse.Results.LastNameEn,
        //                LastNameTh = apiResponse.Results.LastNameTh,
        //                Mobile = apiResponse.Results.Mobile,
        //                NameEn = apiResponse.Results.NameEn,
        //                NameTh = apiResponse.Results.NameTh,
        //                PositionId = apiResponse.Results.PositionId,
        //                SupervisorId = apiResponse.Results.SupervisorId,
        //                EmploymentDate = apiResponse.Results.EmploymentDate,
        //                TerminationDate = apiResponse.Results.TerminationDate
        //            };
        //            await AddEmployee(mEmployee);
        //        }

        //        // var Getemployee = await _employeeRepository.GetByIdAsync(EmpId);
        //        return await _employeeRepository.GetByIdAsync(EmpId);
        //    }
        //    else
        //    {
        //        return employee;
        //    }


        //}

        public async Task AddEmployee(MEmployee employee)
        {
            await _employeeRepository.AddAsync(employee);
        }

        public async Task UpdateEmployee(MEmployee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        public async Task<BusinessUnitsEmployeeApiResponse> GetEmployeeByOrganization(string? businessUnitId)
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeeByOrganization(businessUnitId);
                if (employees == null)
                {
                    return null;
                }

                var response = new BusinessUnitsEmployeeApiResponse
                {
                    Results = employees.Select(emp => new BusinessUnitsEmployeeResult
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
                    }).ToList()
                };

                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task GetEmployeeByBatchEndOfDay(searchEmployeeModels Models)
        {
            try
            {
                var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "employee-all" });
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

             

                    try
                    {
                        var resultApi = await _serviceApi.GetDataApiAsync_EmpPloyee(apiParam, Models.page, Models.perPage);
                    if (resultApi == null || resultApi.Results == null)
                    {

                    }
                    else 
                    {
                        foreach (var item in resultApi.Results)
                        {
                            //if ("SME_44002" == item.EmployeeId.ToString())
                            //{
                            //    string xxxx = "555";
                            //}
                            var EmpX = await GetEmployeeById(item.EmployeeId.ToString());
                            if (EmpX == null)
                            {
                                MEmployee mEmployee = new MEmployee
                                {
                                    EmployeeId = item.EmployeeId,
                                    BusinessUnitId = item.BusinessUnitId,
                                    CompanyId = item.CompanyId,
                                    Email = item.Email,
                                    EmployeeCode = item.EmployeeCode,
                                    EmployeeStatus = item.EmployeeStatus,
                                    EmployeeType = item.EmployeeType,
                                    FirstNameEn = item.FirstNameEn,
                                    FirstNameTh = item.FirstNameTh,
                                    LastNameEn = item.LastNameEn,
                                    LastNameTh = item.LastNameTh,
                                    Mobile = item.Mobile,
                                    NameEn = item.NameEn,
                                    NameTh = item.NameTh,
                                    PositionId = item.PositionId,
                                    SupervisorId = item.SupervisorId,
                                    EmploymentDate = item.EmploymentDate,
                                    TerminationDate = item.TerminationDate,
                                     Createdate = DateTime.Now,
                                     PositionNameEn = item.PositionNameEn,
                                        PositionNameTh = item.PositionNameTh,
                                        JobLevelId = item.JobLevelId,
                                        JobLevelNameEn = item.JobLevelNameEn,
                                        JobLevelNameTh = item.JobLevelNameTh,


                                };

                                await AddEmployee(mEmployee);
                            }
                            else
                            {
                                // update data by emp
                                EmpX.EmployeeId = item.EmployeeId;
                                EmpX.PositionId = item.PositionId;
                                EmpX.PositionNameEn = item.PositionNameEn;
                                EmpX.PositionNameTh = item.PositionNameTh;
                                EmpX.JobLevelId = item.JobLevelId;
                                EmpX.JobLevelNameEn = item.JobLevelNameEn;
                                EmpX.JobLevelNameTh = item.JobLevelNameTh;
                                EmpX.BusinessUnitId = item.BusinessUnitId;
                                EmpX.CompanyId = item.CompanyId;
                                EmpX.Email = item.Email;
                                EmpX.EmployeeCode = item.EmployeeCode;
                                EmpX.EmployeeStatus = item.EmployeeStatus;
                                EmpX.EmployeeType = item.EmployeeType;
                                EmpX.FirstNameEn = item.FirstNameEn;
                                EmpX.FirstNameTh = item.FirstNameTh;
                                EmpX.LastNameEn = item.LastNameEn;
                                EmpX.LastNameTh = item.LastNameTh;
                                EmpX.Mobile = item.Mobile;
                                EmpX.NameEn = item.NameEn;
                                EmpX.NameTh = item.NameTh;
                                EmpX.SupervisorId = item.SupervisorId;
                                EmpX.EmploymentDate = item.EmploymentDate;
                                EmpX.TerminationDate = item.TerminationDate;
                                // update data
                                await UpdateEmployee(EmpX);
                            }

                            try
                            {          ////insert t_employeeprofile   address
                                /////
                                var EmpProfileX = await _profileService.GetProfileById(item.EmployeeId.ToString());

                                ////Insert T_EmpMovement
                                await _movementService.UpsertEmployeeMovement(item.EmployeeId.ToString());

                                ////insert M_EmpByid
                                await _memployeeByIdService.GetEmployeeById(item.EmployeeId.ToString());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }

                       
                        }
                        var newpagemodels = new searchEmployeeModels
                        {

                            page = Models.page + 1,
                            perPage = Models.perPage,

                        };
                        if (resultApi.Results.Count != 0)
                        {
                            await GetEmployeeByBatchEndOfDay(newpagemodels);
                        }
                    }

                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
           

            }
            catch (Exception ex) { }


        }
        public async Task<IEnumerable<MEmployee>> SearchEmployee(MEmployeeModels searchModel)
        {
            try
            {
                var employees = await _employeeRepository.SearchEmployee(searchModel);
                if (employees == null)
                {
                    return null;
                }
                else
                {
                    return employees;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
