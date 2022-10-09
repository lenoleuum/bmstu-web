using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mbti_web.Models;
using mbti_web.Models.Repository;

namespace mbti_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryUser _repuser;

        public UsersController(IRepositoryUser repuser)
        {
            _repuser = repuser;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _repuser.GetAll(); // return 200 (OK)
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            var u = _repuser.Find(id);

            if (u == null)
            {
                return NotFound();
            }

            return new ObjectResult(u); // return 200 (OK)
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _repuser.Add(user);

            return CreatedAtAction(nameof(GetUserByID), new { id = user.Useruk }, user); // return 201 (CREATED)
        }

        // PATCH: api/Users/5
        [HttpPatch("{id}")]
        public IActionResult UpdateUserTelegram([FromBody] User user, int id) 
        {
            if (user == null)
            {
                return BadRequest();
            }

            var u = _repuser.Find(id);

            if (u == null)
            {
                return NotFound();
            }

            _repuser.Update(user);

            return NoContent();  //return 204(NO CONTENT)
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var u = _repuser.Find(id);

            if (u == null)
            {
                return NotFound();
            }

            _repuser.Remove(u);

            return NoContent();  //return 204(NO CONTENT)
        }

        /*
        private readonly mbti_dbContext _context;
        //private readonly RepositoryUser _repuser;

        public UsersController(mbti_dbContext context)
        {
            //_repuser = repuser;
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Useruk }, user);
        }

        // PATCH: api/Users/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<User>> PatchUser(User user, int id)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var u = _context.Users.Find(id);

            if (u == null)
            {
                return NotFound();
            }

            u.Telagram = user.Telagram;
            u.Email = user.Email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Useruk == id);
        }*/
    }
}
