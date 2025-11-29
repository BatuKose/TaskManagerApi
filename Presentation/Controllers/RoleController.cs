using Entites.Data_Transfer_object.Role;
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
    [Route("Role")]
    public class RoleController:ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public RoleController(IServiceManager serviceManager)
        {
            this.serviceManager=serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> InserDto([FromBody] InsertOrUpdateRoleDTO insertRoleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result= await serviceManager.RoleManager.InsertRoleAsync(insertRoleDTO);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetRoleById([FromQuery] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result=await serviceManager.RoleManager.GetRoleByİdAsync(id);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRoleAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await serviceManager.RoleManager.DeleteRoleAsync(id);
            return Ok("Rol silindi");
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<InsertOrUpdateRoleDTO>> UpdateRoleAsync([FromRoute] int id,[FromBody] InsertOrUpdateRoleDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await serviceManager.RoleManager.UpdateRoleAsync(id, dto);
            return Ok(new
            {
                Message = "Rol başarıyla güncellendi.",
                Data = updated
            });
        }

    }
}
