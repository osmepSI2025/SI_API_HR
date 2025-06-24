using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Repository;

namespace SME_API_HR.Services
{
    public class TEmployeeMovementService : ITEmployeeMovementService
    {
        private readonly ITEmployeeMovementRepository _movementRepository;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly ICallAPIService _serviceApi;

        public TEmployeeMovementService(ITEmployeeMovementRepository movementRepository, IApiInformationRepository repositoryApi, ICallAPIService serviceApi)
        {
            _movementRepository = movementRepository;
            _repositoryApi = repositoryApi;
            _serviceApi = serviceApi;
        }

        public async Task<IEnumerable<TEmployeeMovement>> GetAllMovements()
        {
            return await _movementRepository.GetAllAsync();
        }

        public async Task<ApiListEmployeeMovementResponse> GetMovementById(string employeeId)
        {
            var response = new ApiListEmployeeMovementResponse();
            var movement = await _movementRepository.GetByIdAsync(employeeId);

            if (movement != null)
            {
                response.Results = new List<EmployeeMovementResult>
        {
            new EmployeeMovementResult
            {
                Id = movement.Id,
                EmployeeId = movement.EmployeeId,
                EffectiveDate = movement.EffectiveDate,
                MovementTypeId = movement.MovementTypeId,
                MovementReasonId = movement.MovementReasonId,
                EmployeeCode = movement.EmployeeCode,
                Employment = movement.Employment,
                EmployeeStatus = movement.EmployeeStatus,
                EmployeeTypeId = movement.EmployeeTypeId,
                PayrollGroupId = movement.PayrollGroupId,
                CompanyId = movement.CompanyId,
                BusinessUnitId = movement.BusinessUnitId,
                PositionId = movement.PositionId,
                WorkLocationId = movement.WorkLocationId,
                CalendarGroupId = movement.CalendarGroupId,
                JobTitleId = movement.JobTitleId,
                JobLevelId = movement.JobLevelId,
                JobGradeId = movement.JobGradeId,
                ContractStartDate = movement.ContractStartDate,
                ContractEndDate = movement.ContractEndDate,
                RenewContractCount = movement.RenewContractCount,
                ProbationDate = movement.ProbationDate,
                ProbationDuration = movement.ProbationDuration,
                ProbationResult = movement.ProbationResult,
                ProbationExtend = movement.ProbationExtend,
                EmploymentDate = movement.EmploymentDate,
                JoinDate = movement.JoinDate,
                OccupationDate = movement.OccupationDate,
                TerminationDate = movement.TerminationDate,
                TerminationReason = movement.TerminationReason,
                TerminationSSO = movement.TerminationSso,
                IsBlacklist = movement.IsBlacklist,
                PaymentDate = movement.PaymentDate,
                Remark = movement.Remark,
                ServiceYearAdjust = movement.ServiceYearAdjust,
                SupervisorCode = movement.SupervisorCode,
                StandardWorkHoursID = movement.StandardWorkHoursId,
                WorkOperation = movement.WorkOperation
            }
        };
            }
            else
            {
                response.Results = new List<EmployeeMovementResult>();
            }

            return response;
        }


        public async Task AddMovement(TEmployeeMovement movement)
        {
            await _movementRepository.AddAsync(movement);
        }

        public async Task UpdateMovement(TEmployeeMovement movement)
        {
            await _movementRepository.UpdateAsync(movement);
        }

        public async Task DeleteMovement(string employeeId)
        {
            await _movementRepository.DeleteAsync(employeeId);
        }
        public async Task<IEnumerable<TEmployeeMovement>> SearchMovements(TEmployeeMovementModels searchModel)
        {
            return await _movementRepository.SearchAsync(searchModel);
        }

        public async Task<TEmployeeMovement> GetTEmployeeMovementsById(int id)
        {
            return await _movementRepository.GetTEmployeeMovementsById(id);
        }



        public async Task UpsertEmployeeMovement(string EmpId)
        {
            try
            {

              
                    //call api to get employee details


                    var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "employee-movement" });
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
                   
                    var apiResponse = await _serviceApi.GetDataEmpMovementByEmpId(apiParam, EmpId);
                    if (apiResponse != null)
                    {
                        foreach (var item in apiResponse.Results)
                        {
                            
                            var existingEmpMovement = await _movementRepository.GetTEmployeeMovementsById(item.Id);
                            if (existingEmpMovement == null)
                            {
                                TEmployeeMovement mEmployee = new TEmployeeMovement
                                {
                                    CreateDate = DateTime.Now,
                                    Id = item.Id,
                                    EmployeeId = item.EmployeeId,
                                    EffectiveDate = item.EffectiveDate,
                                    MovementTypeId = item.MovementTypeId,
                                    MovementReasonId = item.MovementReasonId,
                                    EmployeeCode = item.EmployeeCode,
                                    Employment = item.Employment,
                                    EmployeeStatus = item.EmployeeStatus,
                                    EmployeeTypeId = item.EmployeeTypeId,
                                    PaymentDate = item.PaymentDate,
                                    CompanyId = item.CompanyId,
                                    BusinessUnitId = item.BusinessUnitId,
                                    PositionId = item.PositionId,
                                    WorkLocationId = item.WorkLocationId,
                                    CalendarGroupId = item.CalendarGroupId,
                                    JobTitleId = item.JobTitleId,
                                    JobLevelId = item.JobLevelId,
                                    JobGradeId = item.JobGradeId,
                                    ContractEndDate = item.ContractEndDate,
                                    ContractStartDate = item.ContractStartDate,

                                    EmploymentDate = item.EmploymentDate,
                                    IsBlacklist = item.IsBlacklist,
                                    JoinDate = item.JoinDate,
                                    OccupationDate = item.OccupationDate,
                                    PayrollGroupId = item.PayrollGroupId,
                                    ProbationDate = item.ProbationDate,
                                    ProbationDuration = item.ProbationDuration,
                                    ProbationExtend = item.ProbationExtend,
                                    ProbationResult = item.ProbationResult,
                                    Remark = item.Remark,
                                    RenewContractCount = item.RenewContractCount,
                                    ServiceYearAdjust = item.ServiceYearAdjust,
                                    StandardWorkHoursId = item.StandardWorkHoursId,
                                    SupervisorCode = item.SupervisorCode,
                                    TerminationDate = item.TerminationDate,
                                    TerminationReason = item.TerminationReason,
                                    TerminationSso = item.TerminationSso,

                                };
                                await AddMovement(mEmployee);
                            }

                        }
                    }
                  
             
               
             
            }
            catch (Exception ex)
            {
              
            }

        }

    }
}
