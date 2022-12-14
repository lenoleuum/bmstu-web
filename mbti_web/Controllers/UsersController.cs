using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using mbti_web.Models;
using mbti_web.Entities;
using mbti_web.Middleware;
using mbti_web.Services;

namespace mbti_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/users/authenticate
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model) // ModelState.IsValid;
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Error! Username or password is incorrect!" });

            return Ok(response);
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel userModel)
        {
            var response = _userService.Register(userModel); //await 

            if (response == null)
            {
                return BadRequest(new { message = "Error! Didn't register!" });
            }

            return Ok(response);
        }

        // GET: api/Users
        [Authorize()]
        [HttpGet]
        public IActionResult GetUsers() //  claim
        {
            var users = _userService.GetAll(); 
            return Ok(users); // return 200 (OK)
        }
        
        // GET: api/Users/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            var u = _userService.GetById(id);

            if (u == null)
            {
                return NotFound();
            }

            return new ObjectResult(u); // return 200 (OK)
        }

        // POST: api/Users
        [Authorize]
        [HttpPost]
        public IActionResult AddUser([FromBody] UserModel userModel)
        {
            if (userModel == null)
            {
                return BadRequest();
            }

            _userService.AddUser(userModel);

            return CreatedAtAction(nameof(GetUserByID), new { id = userModel.ID }, userModel); // return 201 (CREATED)
        }

        // PATCH: api/Users/5
        //[Authorize]
        [Authorize("admin")] // в jwt
        [HttpPatch("{id}")]
        public IActionResult UpdateUserTelegram([FromBody] UserModel userModel, int id) 
        {
            if (userModel == null)
            {
                return BadRequest();
            }

            var u = _userService.GetById(id);

            if (u == null)
            {
                return NotFound();
            }

            _userService.UpdateUser(userModel);

            return NoContent();  //return 204(NO CONTENT)
        }

        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var u = _userService.GetById(id);

            if (u == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(u);

            return NoContent();  //return 204(NO CONTENT)
        }
    }
}
