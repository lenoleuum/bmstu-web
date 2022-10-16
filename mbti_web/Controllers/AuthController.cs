using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using mbti_web.Models;
using mbti_web.Repository;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using mbti_web.Entities;
using Microsoft.AspNetCore.Authorization;

namespace mbti_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IRepositoryUser _repuser;
        public AuthController(IRepositoryUser repuser)
        {
            _repuser = repuser;
        }

        private string GenerateJwtToken(string Login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", Login) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = AuthOptions.ISSUER,
                Audience = AuthOptions.AUDIENCE,
                SigningCredentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost(nameof(Authorize))]
        public IActionResult Authorize([FromBody] LoginUser user)
        {
            if (CheckValidity(user))
            {
                var tokenString = GenerateJwtToken(user.Login);

                return Ok(new { Token = tokenString, Message = "Success!" });
            }
            else
                return BadRequest("Please pass the valid Login and Password!");
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetResult))]
        public IActionResult GetResult()
        {
            return Ok("Token Validated!");
        }

        [NonAction]
        public bool CheckValidity(LoginUser user)
        {
            List<User> users = _repuser.GetAll().ToList();

            User u = users.Find(u => u.Login == user.Login && u.Password == user.Password);

            if (u != null)
                return true;
            else
                return false;
        }
    }
}
