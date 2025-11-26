using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.User;
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
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _ServiceManager;
        public UserController(IServiceManager serviceManager)
        {
            _ServiceManager = serviceManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ServiceManager.UserService.CreateUserAsync(userDto);
            return StatusCode(201);
        }
        [HttpPost("GetUserWithRole")]
        public async Task<IActionResult> GetUserWithRole([FromBody] string dto)
        {
            var result = await _ServiceManager.UserService.getUsersAndRoleAsync(dto);
            return Ok(result);
        }
        [HttpGet ("id:int")]
        public async Task<IActionResult> GetUserByid([FromQuery(Name ="id")]int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); 
            var result= await _ServiceManager.UserService.getUserByIdAsync(id);
            return Ok(result);
        }
    }
}
