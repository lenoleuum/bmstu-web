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
    public class CharactersController : ControllerBase
    {
        private IRepositoryCharacter _repchar;

        public CharactersController(IRepositoryCharacter repchar)
        {
            _repchar = repchar;
        }

        // GET: api/Characters
        [HttpGet]
        public IEnumerable<Character> GetCharacters()
        {
            return _repchar.GetAll(); // return 200 (OK)
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public IActionResult GetCharacterByID(int id)
        {
            var c = _repchar.Find(id);

            if (c == null)
            {
                return NotFound();
            }

            return new ObjectResult(c);  // return 200 (OK)
        }

        // POST: api/Characters
        [HttpPost]
        public IActionResult AddCharacter([FromBody] Character character)
        {
            if (character == null)
            {
                return BadRequest();
            }

            _repchar.Add(character);

            return CreatedAtAction(nameof(GetCharacterByID), new { id = character.Characteruk }, character); // return 201 (CREATED) 
        }

        // PATCH: api/Characters/5
        [HttpPatch("{id}")]
        public IActionResult UpdateCharacterType([FromBody] Character character, int id)
        {
            if (character == null)
            {
                return BadRequest();
            }

            var c = _repchar.Find(id);

            if (c == null)
            {
                return NotFound();
            }

            _repchar.Update(character);

            return NoContent();  //return 204(NO CONTENT)
        }
    }
}
