using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using mbti_web.Models;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace mbti_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly mbti_dbContext _context;

        public AuthController(mbti_dbContext context)
        {
            _context = context;
        }
        [HttpGet("/auth")]
        public IActionResult Authorize()
        {
            // на стороне клиента нужно получить данные из формы (логин/пароль),
            // отправить запрос POST /auth?username=lgin&password=password
            // и получить ответ: если true, то все ок, авторизация успешна,
            // если нет, то sorry

            return null;
        }

        [HttpPost("/auth")]
        public IActionResult Token(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;

            // создаем JWT-токена
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            var result = _context.Users.Where(x => x.Login == login && x.Password == password);
            User u = result.FirstOrDefault();

            if (u != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, u.Login)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // пользователя нет в базе
            return null;
        }
    }
}
