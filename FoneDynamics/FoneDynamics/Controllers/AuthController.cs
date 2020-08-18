using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using FoneDynamics.Helpers;
using FoneDynamics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoneDynamics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost("token")]
        public IActionResult GetToken([FromBody] Auth auth)
        {

            if (!AuthRepository.Validate(auth.Username, auth.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            var signingCredentials = new SigningCredentials(AuthConfiguration.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: AuthConfiguration.ValidIssuer,
                audience: AuthConfiguration.ValidAudience,
                expires: AuthConfiguration.JwtTokenExpiration,
                signingCredentials: signingCredentials
                );

            return Ok(new
            {
                username = auth.Username,
                token = new JwtSecurityTokenHandler().WriteToken(token)

            });
        }
    }

}