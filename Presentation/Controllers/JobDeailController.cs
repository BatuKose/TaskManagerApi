using Entites.Data_Transfer_object.JobDetail;
using Entites.Data_Transfer_object.JobHeader;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/JobDetail")]
    public class JobDeailController:ControllerBase
    {
        private readonly IServiceManager _ServiceManager;
        public JobDeailController(IServiceManager serviceManager)
        {
            _ServiceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> InsertJobAsync([FromBody] InsertJobDetailDTO jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ServiceManager.JobDetailService.InsertJobDetailAsync(jobDto);
            return StatusCode(201);
        }
    }
}
