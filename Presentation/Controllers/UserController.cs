using Entites.Data_Transfer_object;
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
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto userDto )
        {
            await _ServiceManager.UserService.CreateUserAsync(userDto);
            return  StatusCode(201,userDto);
        }
    }
}
