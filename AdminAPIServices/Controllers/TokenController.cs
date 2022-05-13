using AdminAPIServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenManager _jwtTokenManager;
        public TokenController(IJwtTokenManager jwtTokenManager)
        {
            _jwtTokenManager = jwtTokenManager;
        }

        //[AllowAnonymous]
        //[HttpPost("Authenticate")]
        //public IActionResult Authenticate([FromBody]tblLogin login)
        //{
        //    var token = _jwtTokenManager.Authenticate(login.EmailID, login.Password, login.Role);
        //    if (string.IsNullOrEmpty(token))
        //        return Unauthorized();
        //    return Ok(token);
        //}

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public TokenResult Authenticate([FromBody] tblLogin login)
        {
            TokenResult tokenResult = new TokenResult();

            tokenResult.message = _jwtTokenManager.Authenticate(login.EmailID, login.Password, login.Role);
            if (string.IsNullOrEmpty(tokenResult.message))
            {
                tokenResult.message = "Unauthorized";
                return tokenResult;
            }


            return tokenResult;
        }

    }

    public class TokenResult
    {
        public string message;

    }
}
