using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.JobHeader;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        public async Task<IActionResult> CreateJobAsync([FromBody] CreateJobHeaderDTO jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ServiceManager.JobHeaderService.InsertJobInsertJobHeader(jobDto);
            return StatusCode(201);
        }

    }
}
