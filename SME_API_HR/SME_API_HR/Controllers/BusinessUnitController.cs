using Microsoft.AspNetCore.Mvc;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Controllers
{
    [Route("api/SYS-HR/org")]
    [ApiController]
    public class BusinessUnitController : ControllerBase
    {
        private readonly IBusinessUnitService _businessUnitService;
        private readonly ITOrganizationTreeService _organizationTreeService;
        private readonly IEmployeeService _employeeService;

        public BusinessUnitController(IBusinessUnitService businessUnitService, ITOrganizationTreeService organizationTreeService, IEmployeeService employeeService)
        {
            _businessUnitService = businessUnitService;
            _organizationTreeService = organizationTreeService;
            _employeeService = employeeService;
        }

        // GET: api/BusinessUnit
        [HttpGet("business-units")]
        public async Task<ActionResult<IEnumerable<BusinessUnitApiResponse>>> GetAll()
        {
            return Ok(await _businessUnitService.GetAllBusinessUnits());
        }

        // GET: api/BusinessUnit/{id}
        [HttpGet("business-units/{id}")]
        public async Task<ActionResult<BusinessUnitApiResponse>> GetById(string id)
        {
            var businessUnit = await _businessUnitService.GetBusinessUnitById(id);
            return businessUnit != null ? Ok(businessUnit) : NotFound();
        }


        // GET: api/business-units/{BusinessUnitId}/employees
        [HttpGet("{id}/employees")]
        public async Task<ActionResult<List<BusinessUnitsEmployeeApiResponse>>> GetEmployeeByOrganization(string id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByOrganization(id);
                return employee != null ? Ok(employee) : NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }



        }

        // GET: api/BusinessUnit
        [HttpGet("BusinessUnit-BatchEndOfDay")]
        public async Task<ActionResult> BatchEndOfDay()
        {
            await _businessUnitService.BatchEndOfDay();
            return Ok();
        }
      
        [HttpPost("BusinessUnit-Search")]
        public async Task<ActionResult<IEnumerable<MBusinessUnit>>> Search([FromBody] MBusinessUnitModels searchModel)
        {
            var results = await _businessUnitService.SearchBusinessUnits(searchModel);
            return Ok(results);
        }

        #region Org-tree
        [HttpGet("organization")]
        public async Task<ActionResult<IEnumerable<TOrganizationTree>>> GetAllOrganizationTrees()
        {
            try
            {
                return Ok(await _organizationTreeService.GetAllOrganizationTrees());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("organization-tree")]
        public async Task<IActionResult> GetOrganizationTreeHierarchy()
        {
            try
            {
                var result = await _organizationTreeService.GetOrganizationTreeHierarchy();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/organization-trees
        [HttpGet("organization-trees-BatchEndOfDay")]
        public async Task<ActionResult> OrgTreeBatchEndOfDay()
        {
            await _organizationTreeService.BatchEndOfDay();
            return Ok();
        }

        #endregion 
    }
}
