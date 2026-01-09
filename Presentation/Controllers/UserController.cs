using Entites.Data_Transfer_object;
using Entites.Data_Transfer_object.User;
using Entites.Data_Transfer_object.UserIzinDetay;
using Entites.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _ServiceManager;
        public UserController(IServiceManager serviceManager)
        {
            _ServiceManager = serviceManager;
        }
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ServiceManager.UserService.CreateUserAsync(userDto);
            return StatusCode(201);
        }
        [Authorize(Policy = "Admin-Manage")]
        [HttpPost("GetUserWithRole")]
        public async Task<IActionResult> GetUserWithRole([FromBody] string dto)
        {
            var result = await _ServiceManager.UserService.getUsersAndRoleAsync(dto);
            return Ok(result);
        }
        [Authorize(Policy = "Admin-Manage")]
        [HttpGet ("id:int")]
        public async Task<IActionResult> GetUserByid([FromQuery(Name ="id")]int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); 
            var result= await _ServiceManager.UserService.getUserByIdAsync(id);
            return Ok(result);
        }
        [Authorize(Policy = "Admin")]
        [HttpPatch("softdelete")]
        public async Task<IActionResult> UserSoftDelete([FromQuery(Name ="id")] int id)
        {
          await  _ServiceManager.UserService.UserSoftDeleteAsync(id);
            return NoContent();
        }
        [Authorize(Policy = "Admin")]
        [HttpPatch("updateUser")]
        public async Task<IActionResult> UpdateUserAsync([FromQuery ] int id,[FromBody] UpdateUserDto userDto)
        {
           if(!ModelState.IsValid) return BadRequest(ModelState);
           await _ServiceManager.UserService.UpdateUserAsync(id, userDto);
            return Ok(userDto);
        }
        
        [Authorize(Policy = "Admin-Manage")]
        [HttpPost("izinEkle")]
        public async Task<IActionResult> IzınEkleAsync([FromQuery(Name ="id")] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            await _ServiceManager.UserService.IzınEkleAsync(id);
            return Ok("izin eklendi");
        }
        [AllowAnonymous]
      //  [Authorize(Policy = "Admin-Manage")]
        [HttpPost("izinDetayEkle")]
        public async Task<IActionResult> UserDetayIzinEkleAsync([FromBody] UserIzinDetayEkleDTO userIzinDetayEkle  )
        {
           if(!ModelState.IsValid) return BadRequest(ModelState);
           await _ServiceManager.UserService.UserIzinDetayEkleAsync( userIzinDetayEkle);
            return Ok("İzin Detay Eklendi");
        }
    }
}
