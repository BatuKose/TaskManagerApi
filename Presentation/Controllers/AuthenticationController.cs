using Entites.Data_Transfer_object.User;
using Entites.Models;
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
    [Route("Authentication")]
    [Authorize]
    public class AuthenticationController:ControllerBase
    {
       
        private readonly IServiceManager serviceManager;

        public AuthenticationController(IServiceManager serviceManager)
        {
            this.serviceManager=serviceManager;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var token=serviceManager.authenticationService.LoginAsync(login);
            return Ok(new {token=token});
        }
    }
}
