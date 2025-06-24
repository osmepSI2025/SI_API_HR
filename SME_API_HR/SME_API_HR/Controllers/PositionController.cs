using Microsoft.AspNetCore.Mvc;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Controllers
{
    [Route("api/SYS-HR/Position")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IMPositionService _positionService;

        public PositionController(IMPositionService positionService)
        {
            _positionService = positionService;
        }

        // GET: api/Position
        [HttpGet]
        public async Task<ActionResult<ApiListPositionResponse>> GetAll()
        {
            try
            {
                var result = await _positionService.GetAllPositions();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return StatusCode(500, "An error occurred while retrieving positions.");
            }
        }

        // GET: api/Position/{id}
        [HttpGet("{code}")]
        public async Task<ActionResult<ApiPositionResponse>> GetById(string code)
        {
            try
            {
                var position = await _positionService.GetPositionById(code);
                return position != null ? Ok(position) : NotFound();
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return StatusCode(500, "An error occurred while retrieving positions.");
            }
           
        }

      
        // GET: api/BusinessUnit
        [HttpGet("Position-BatchEndOfDay")]
        public async Task<ActionResult> BatchEndOfDay()
        {
            await _positionService.BatchEndOfDay();
            return Ok();
        }

        [HttpPost("Position-Search")]
        public async Task<ActionResult<IEnumerable<MPosition>>> Search([FromBody] MPositionModels searchModel)
        {
            var results = await _positionService.SearchPosition(searchModel);
            return Ok(results);
        }
    }
}
