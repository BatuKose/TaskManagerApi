using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.JobDetail;
using Entites.Data_Transfer_object.JobHeader;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    public class JobController : ControllerBase
    {
        private readonly IServiceManager _ServiceManager;
        public JobController(IServiceManager serviceManager)
        {
            _ServiceManager = serviceManager;
        }

        //  [Authorize(Policy = "Manager")]
        [HttpPost("isbaslik")]

        public async Task<IActionResult> CreateJobAsync([FromBody] CreateJobHeaderDTO jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ServiceManager.JobHeaderService.InsertJobInsertJobHeader(jobDto);
            return StatusCode(201);
        }

        //[Authorize(Policy = "Manager")]
        [HttpDelete("iş başlık")]
        public async Task<IActionResult> DeleteJobHeaderAsync([FromQuery] int id)
        {
            await _ServiceManager.JobHeaderService.DeleteJobHeader(id);
            return NoContent();
        }

        //[Authorize(Policy = "Worker-Manager")]
        [HttpGet("iş başlık")]
        public IActionResult SelectJob([FromQuery] int id)
        {
            var result = _ServiceManager.JobHeaderService.SelectJobHeader(id);
            return Ok(result);
        }
        [HttpGet("BütünisBaslık")]
        public async Task<IActionResult> SelectJobAll([FromQuery] bool? bitmisMi)
        {
            var result = await _ServiceManager.JobHeaderService.SelectJobHeaderAll(bitmisMi);
            return Ok(result);
        }

        //[Authorize(Policy = "Worker")]
        [HttpPost("karsila")]
        public async Task<IActionResult> IsKarsila(int userId, int jobId)
        {
            var result = await _ServiceManager.JobHeaderService.Iskarsila(userId, jobId);

            return Ok(new
            {
                message = "İş karşılandı"
            });
        }

        //[Authorize(Policy = "Manager")]
        [HttpPut("iş başlık")]
        public async Task<IActionResult> JobHeaderGuncelle([FromQuery] int id, [FromBody] updateJobHeaderDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _ServiceManager.JobHeaderService.updatejobHeader(id, dto);
            return NoContent();
        }

        // [Authorize(Policy = "Worker")]
        [HttpPost("iş detay")]
        public async Task<IActionResult> InsertJobAsync([FromBody] InsertJobDetailDTO jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ServiceManager.JobDetailService.InsertJobDetailAsync(jobDto);
            return NoContent();
        }

        //   [Authorize(Policy = "Worker")]
        [HttpDelete("isdetay")]
        public async Task<IActionResult> DeleteJobDetail([FromQuery] int jobDetayId)
        {
            var result = await _ServiceManager.JobDetailService.DeleteJobDetailAsync(jobDetayId);
            return NoContent();
        }

        // [Authorize(Policy = "Worker-Manager")]
        [HttpGet("jobAllDetails")]
        public IActionResult GetJobWithAllDetails([FromQuery] int jobHeaderId)
        {
            var result = _ServiceManager.JobDetailService.SelectJobDetaiAllDetail(jobHeaderId);
            return Ok(result);
        }

        //[Authorize(Policy = "Manager")]
        [HttpGet("Cezalıİsler")]
        public IActionResult GetCezaliIsler()
        {
            var result = _ServiceManager.JobDetailService.GetCezalıİsler();
            return Ok(result);
        }
        // [Authorize(Policy = "Worker-Manager")]
        [HttpGet("Bütünisler")]
        public async Task<IActionResult> BütünIsleriGetirAsync([FromQuery] bool? isActive)
        {
            var result = await _ServiceManager.JobDetailService.BütünİsleriGetirAsync(isActive);
            return Ok(result);
        }
        // [Authorize(Policy = "Worker-Manager")]
        [HttpGet("Kendiİslerim")]
        public async Task<IActionResult> KendiIslerimAsync([FromQuery] int userId)
        {
            var result = await _ServiceManager.JobHeaderService.FindJobHeaderAsync(userId);
            return Ok(result);
        }
    }
}
