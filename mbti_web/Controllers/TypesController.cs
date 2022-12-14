using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mbti_web.Models;
using mbti_web.Repository;
using mbti_web.Entities;
using mbti_web.Services;

using Type = mbti_web.Entities.Type;
using mbti_web.Middleware;

namespace mbti_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly ITypeService _typeService;

        public TypesController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        // GET: api/Types
        [Authorize]
        [HttpGet]
        public IActionResult GetTypes(string? name = "")
        {
            if (name == "")
            {
                var types = _typeService.GetAllTypes();
                return Ok(types); // return 200 (OK)
            }
            else
            {
                var types = _typeService.GetTypeByNameLike(name);
                return Ok(types); // return 200 (OK)
            }
        }

        // GET: api/Types/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetTypeByID(int id)
        {
            var t = _typeService.GetTypeByID(id);

            if (t == null)
            {
                return NotFound();
            }

            return new ObjectResult(t); // return 200 (OK)
        }

        // PATCH: api/Types/5
        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult UpdateTypeDesc([FromBody] TypeModel typeModel, int id)
        {
            if (typeModel ==  null)
            {
                return BadRequest();
            }

            var t = _typeService.GetTypeByID(id);

            if (t == null)
            {
                return NotFound();
            }

            _typeService.UpdateDesc(typeModel);

            return NoContent();  //return 204(NO CONTENT)
        }
    }
}
