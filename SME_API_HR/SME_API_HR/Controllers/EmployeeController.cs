using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Repository;
using SME_API_HR.Services;

namespace SME_API_HR.Controllers
{
    [Route("api/SYS-HR/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITEmployeeProfileService _profileService;
        private readonly ITEmployeeMovementService _movementService;
        private readonly IMEmployeeByIdService _memployeeByIdService;
        private readonly ITEmployeeContractService _employeeContractService;

        public EmployeeController(IEmployeeService employeeService, ITEmployeeProfileService profileService, ITEmployeeMovementService movementService, IMEmployeeByIdService memployeeByIdService
            , ITEmployeeContractService employeeContractService
            )
        {
            _employeeService = employeeService;
            _profileService = profileService;
            _movementService = movementService;
            _memployeeByIdService = memployeeByIdService;
            _employeeContractService = employeeContractService;


        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiListEmployeeResponse>>> GetAll(int page,int perPage)
        {
            var smodel = new searchEmployeeModels 
            {
             page = page,
             perPage = perPage
            };
            return Ok(await _employeeService.GetAllEmployees(smodel));
        }

        // GET: api/Employee/{employeeId}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiEmployeeResponse>> GetById(string id)
        {
            try
            {
                var employee = await _memployeeByIdService.GetEmployeeById(id);
                return employee != null ? Ok(employee) : new ApiEmployeeResponse();
            }
            catch (Exception ex)
            {
                return new ApiEmployeeResponse();
            }



        }

        
        [HttpGet("Employee-BatchEndOfDay")]
        public async Task<IActionResult> GetEmployeeByBatchEndOfDay()
        {
            var Models = new searchEmployeeModels
            {
                page = 1,
                perPage = 100
            };
            try
            {
                await _employeeService.GetEmployeeByBatchEndOfDay(Models);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }



        }
      
        //[HttpGet("profiles")]
        //public async Task<ActionResult<IEnumerable<TEmployeeProfile>>> GetAllProfiles()
        //{
        //    return Ok(await _profileService.GetAllProfiles());
        //}

        [HttpGet("{empId}/profile")]
        public async Task<ActionResult<ApiEmployeeProfileResponse>> GetProfileById(string empId)
        {
            var profile = await _profileService.GetProfileById(empId);
            return profile != null ? Ok(profile) : NotFound();
        }
        [HttpPost("Employee-Search")]
        public async Task<ActionResult<IEnumerable<MEmployee>>> Search([FromBody]MEmployeeModels searchModel)
        {
            var results = await _employeeService.SearchEmployee(searchModel);
            return Ok(results);
        }


        #region Emp movement
        //[HttpGet("movements")]
        //public async Task<ActionResult<IEnumerable<TEmployeeMovement>>> GetAllMovements()
        //{
        //    return Ok(await _movementService.GetAllMovements());
        //}

        [HttpGet("{employeeId}/movements")]
        public async Task<ActionResult<ApiListEmployeeMovementResponse>> GetMovementById(string employeeId)
        {
            var movement = await _movementService.GetMovementById(employeeId);
            return movement != null ? Ok(movement) : NotFound();
        }


        [HttpPost("movements-search")]
        public async Task<ActionResult<IEnumerable<TEmployeeMovement>>> SearchMovements([FromBody] TEmployeeMovementModels searchModel)
        {
            var results = await _movementService.SearchMovements(searchModel);
            return Ok(results);
        }


        //GET: api/Employee/{employeeId}
        [HttpGet("Batch-employee-contracts")]
        public async Task<ActionResult> Batch_Econtracts()
        {
            //test datetime
            string dateString = "2022-10-03";
            var msearch = new searchEmployeeContractModels
            {
              //  employmentDate = employmentDate.ToDateTime(TimeOnly.MinValue), // Convert DateOnly to DateTime
              //employmentDate = DateTime.Parse(dateString),
                employmentDate = DateTime.Now.Date,
                page = 1,
                perPage = 100
            };
            try
            {
                await _employeeContractService.BatchEndOfDay(msearch);
                return Ok();
                //  var employee = await _employeeContractService.SearchEmployeeContract(msearch);
                //  return employee != null ? Ok(employee) : NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        //GET: api/Employee/{employeeId}


        [HttpGet("employee-contracts")]
        public async Task<ActionResult<ApiListEmployeeContractResponse>> getEmployeeContract(DateTime employmentDate,int page,int perPage)
        {
            //test datetime
            string dateString = "2022-10-03";
            var msearch = new searchEmployeeContractModels
            {
                //  employmentDate = employmentDate.ToDateTime(TimeOnly.MinValue), // Convert DateOnly to DateTime
                employmentDate = employmentDate,
               
                //employmentDate = DateTime.Now.Date,
                page = page,
                perPage = perPage
            };
            try
            {
               var result = await _employeeContractService.SearchEmployeeContract(msearch);
                return Ok(result);
                //  var employee = await _employeeContractService.SearchEmployeeContract(msearch);
                //  return employee != null ? Ok(employee) : NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("employee-contracts/{employeeId}/{employmentDate}")]
        public async Task<ActionResult<TEmployeeContract>> getEmployeeContractByEmpid(string employeeId, DateTime employmentDate)
        {
            //test datetime
            string dateString = "2022-10-03";
            var msearch = new searchEmployeeContractModels
            {
                //  employmentDate = employmentDate.ToDateTime(TimeOnly.MinValue), // Convert DateOnly to DateTime
                employmentDate = employmentDate,
                EmployeeId = employeeId,
                //employmentDate = DateTime.Now.Date,
                page = 1,
                perPage = 100
            };
            try
            {
                var result = await _employeeContractService.SearchEmployeeContractByEmpId(msearch);
                return Ok(result);
               
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        #endregion
    }
}
