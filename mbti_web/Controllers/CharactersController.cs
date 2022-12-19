using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mbti_web.Models;
using mbti_web.Services;
using mbti_web.Entities;
using mbti_web.Middleware;

namespace mbti_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private ICharacterService _charService;

        public CharactersController(ICharacterService charService)
        {
            _charService = charService;
        }

        // GET: api/Characters or api/Characters?str=ENTP&flag=1
        [Authorize]
        [HttpGet]
        public IActionResult GetCharacters(string? str = "", int? flag = -1) // fromQuery
        {
            if (str == "" && flag == -1)
            {
                var characters = _charService.GetAllCharacters();
                return Ok(characters); // return 200 (OK)
            }
            else
            {
                List<CharacterModel> chars = _charService.GetAllCharacters().ToList();
                List<CharacterModel> result = new List<CharacterModel>();

                switch (flag)
                {
                    case 1:
                        foreach (CharacterModel c in chars)
                        {
                            if (c.Type.ToLower().Contains(str.ToLower()))
                            {
                                result.Add(c);
                            }
                        }

                        break;
                    case 2:
                        foreach (CharacterModel c in chars)
                        {
                            if (c.Category.ToLower().Contains(str.ToLower()))
                            {
                                result.Add(c);
                            }
                        }

                        break;
                    case 3:
                        foreach (CharacterModel c in chars)
                        {
                            if (c.Name.ToLower().Contains(str.ToLower()))
                            {
                                result.Add(c);
                            }
                        }

                        break;
                    default:
                        break;
                }

                return Ok(result);  // return 200 (OK)
            }
        }

        // GET: api/Characters/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetCharacterByID(int id)
        {
            var c = _charService.GetCharacterByID(id);

            if (c == null)
            {
                return NotFound();
            }

            return new ObjectResult(c);  // return 200 (OK)
        }

        // POST: api/Characters
        [Authorize]
        [HttpPost]
        public IActionResult AddCharacter([FromBody] CharacterModel characterModel)
        {
            if (characterModel == null)
            {
                return BadRequest();
            }

            _charService.AddCharacter(characterModel);

            return CreatedAtAction(nameof(GetCharacterByID), new { id = characterModel.ID }, characterModel); // return 201 (CREATED) 
        }

        // PATCH: api/Characters/5
        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult UpdateCharacterType([FromBody] CharacterModel characterModel, int id)
        {
            if (characterModel == null)
            {
                return BadRequest();
            }

            var c = _charService.GetCharacterByID(id);

            if (c == null)
            {
                return NotFound();
            }

            _charService.UpdateType(characterModel);

            return Ok(characterModel);  //return 200 (OK)
        }
    }
}
