using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinAuthentication.Authentication.Windows;
using WinAuthentication.Models;

namespace WinAuthentication.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [Route("api/authenticate/windows")]
        [HttpPost]
        public IActionResult WindowsAuthentice([FromBody] UserRequest userRequest)
        {

            string validateMsg = userRequest.Validate();

            if(string.IsNullOrEmpty(validateMsg))
            {
                WindowsAuthentication windowsAuthentication = new WindowsAuthentication(userRequest);

                if(windowsAuthentication.Authenticate())
                {
                    return Ok(true);
                }
                else
                {
                    return Unauthorized("Invalid username or password");
                }
            }
            else
            {
                return BadRequest(validateMsg);
            }
        }
    }
}
