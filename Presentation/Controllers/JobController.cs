using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.JobDetail;
using Entites.Data_Transfer_object.JobHeader;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Job")]
    public class JobController :ControllerBase
    {
        private readonly IServiceManager _ServiceManager;
        public JobController(IServiceManager serviceManager)
        {
            _ServiceManager = serviceManager;
        }

        [HttpPost("iş başlık")]
        public async Task<IActionResult> CreateJobAsync([FromBody] CreateJobHeaderDTO jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ServiceManager.JobHeaderService.InsertJobInsertJobHeader(jobDto);
            return StatusCode(201);
        }
        [HttpDelete("iş başlık")]
        public async Task<IActionResult> DeleteJobHeaderAsync([FromQuery] int id)
        {
            await _ServiceManager.JobHeaderService.DeleteJobHeader(id);
            return NoContent();
        }
        [HttpGet("iş başlık")]
        public IActionResult SelectJob([FromQuery] int id)
        {
          var result=  _ServiceManager.JobHeaderService.SelectJobHeader(id);
            return Ok(result);
        }
        [HttpPost("karsila")]
        public async Task<IActionResult> IsKarsila(int userId, int jobId)
        {
            var result = await _ServiceManager.JobHeaderService.Iskarsila(userId, jobId);

            return Ok(new
            {
                message = "İş karşılandı"
            });
        }

        [HttpPut("iş başlık")]
        public async Task<IActionResult> JobHeaderGuncelle([FromQuery] int id, [FromBody] updateJobHeaderDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _ServiceManager.JobHeaderService.updatejobHeader(id, dto);
            return Ok(result);
        }
        [HttpPost("iş detay")]
        public async Task<IActionResult> InsertJobAsync([FromBody] InsertJobDetailDTO jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ServiceManager.JobDetailService.InsertJobDetailAsync(jobDto);
            return StatusCode(201);
        }
        [HttpDelete("iş detay")]
        public async Task<IActionResult> DeleteJobDetail([FromQuery] int jobDetayId)
        {
         var result= await  _ServiceManager.JobDetailService.DeleteJobDetailAsync(jobDetayId);
            return NoContent();
        }
        [HttpGet("jobAllDetails")]
        public IActionResult GetJobWithAllDetails([FromQuery] int jobHeaderId)
        {
            var result= _ServiceManager.JobDetailService.SelectJobDetaiAllDetail(jobHeaderId);
            return Ok(result);
        }
    }
}
