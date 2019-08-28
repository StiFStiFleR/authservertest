using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServerTest.Model;
using AuthServerTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServerTest.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect!" });
            }

            return Ok(user);
        }

        public IActionResult GetAll()
        {
            return Ok(userService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("check")]
        public IActionResult Check()
        {
            return Ok(new { message = "fuck"});
        }
    }
}