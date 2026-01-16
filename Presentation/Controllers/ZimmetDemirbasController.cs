using Entites.Data_Transfer_object.ZimmetDemirbas;
using Microsoft.AspNetCore.Authorization;
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
    [Route("ZimmetDemirbas")]
     // [Authorize]
    public class ZimmetDemirbasController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ZimmetDemirbasController(IServiceManager serviceManager)
        {
            _serviceManager=serviceManager;
        }

        [HttpPost("InsertCategory")]
        public  async Task<IActionResult> InsertCategoryAsync([FromBody] CreateCategoryDTO categoryDTO)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            await _serviceManager.ZimmetDemirbasService.CreateCategoryAsync(categoryDTO);
            return Ok("Kategori başarıyla eklendi.");
        }
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryDTO updateCategoryDTO,int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            await _serviceManager.ZimmetDemirbasService.UpdateCategoryAsync(updateCategoryDTO,id);
            return Ok("Kategori başarıyla güncellendi.");
        }
    }
}
