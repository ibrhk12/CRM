using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CRM.BusinessLayer.InputModel;
using CRM.DataAccess;
using CRM.DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CRM.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersManager _userManager;
        private readonly IConfiguration _configuration;
        public AuthController(IUsersManager userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterIM value)
        {
            Users user = new Users {
                firstName = value.firstName,
                lastName = value.lastName,
                userName = value.userName,
                email = value.email,
                password = value.password,
                department = value.department,
                hierarchy = value.hierarchy
            };
            var result = await _userManager.AddUser(user);
            if (!result.emailExist && !result.userNameExist)
            {
               var token = tokenGen(user);
                return Ok(new {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user
                });
            }
            return BadRequest(result.message);
        }
        // POST: api/Auth
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginIM value)
        {
            var user = await _userManager.GetUser(value.userName);
            if (user != null && user.password == value.password)
            {
               var token = tokenGen(user);
                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    }
                    );
            }
            return Unauthorized();
        }
        //token Generator Method
        public JwtSecurityToken tokenGen(Users user)
        {
            var claim = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.userName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
            var signinKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Site"],
                    audience: _configuration["Jwt:Site"],
                    expires: DateTime.UtcNow.AddHours(1),
                    claims: claim,
                    signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                    );
            return token;
        }

    }
}
