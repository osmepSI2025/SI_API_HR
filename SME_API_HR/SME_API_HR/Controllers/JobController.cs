using Microsoft.AspNetCore.Mvc;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Controllers
{
    [Route("api/SYS-HR/Job")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMJobLevelService _jobLevelService;

        public JobController(IJobService jobService,IMJobLevelService JobLevelService)
        {
            _jobService = jobService;
            _jobLevelService = JobLevelService;
        }

        [HttpGet("job-titles")]
        public async Task<ActionResult<IEnumerable<ApiListJobTitleResponse>>> GetAll()
        {
            return Ok(await _jobService.GetAllJobTitles());
        }

        [HttpGet("job-titles/{code}")]
        public async Task<ActionResult<ApiJobTitleResponse>> GetById(string code)
        {
            var jobTitle = await _jobService.GetJobTitleById(code);
            return jobTitle != null ? Ok(jobTitle) : NotFound();
        }

        [HttpGet("job-levels")]
        public async Task<ActionResult<IEnumerable<ApiListJobLevelResponse>>> GetAllJobLevels()
        {
            return Ok(await _jobLevelService.GetAllJobLevels());
        }

        [HttpGet("job-levels/{Code}")]
        public async Task<ActionResult<ApiJobLevelResponse>> GetJobLevelById(string Code)
        {
            var jobLevel = await _jobLevelService.GetJobLevelById(Code);
            return jobLevel != null ? Ok(jobLevel) : NotFound();
        }


        [HttpGet("Job-BatchEndOfDay")]
        public async Task<ActionResult> BatchEndOfDay()
        {
            await _jobService.BatchEndOfDay();
            return Ok();
        }


        [HttpGet("JobLevel-BatchEndOfDay")]
        public async Task<ActionResult> BatchEndOfDayJobLevel()
        {
            await _jobLevelService.BatchEndOfDay();
            return Ok();
        }

        [HttpPost("JobLevel-Search")]
        public async Task<ActionResult<IEnumerable<MJobTitle>>> SearchJobLevel([FromBody] MJobLevelModels searchModel)
        {
            var results = await _jobLevelService.SearchJobLevel(searchModel);
            return Ok(results);
        }
    }
}