using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mbti_web.Models;
using mbti_web.Models.Repository;

using Type = mbti_web.Models.Type;

namespace mbti_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private IRepositoryType _reptype;

        public TypesController(IRepositoryType reptype)
        {
            _reptype = reptype;
        }

        // GET: api/Types
        [HttpGet]
        public IEnumerable<Type> GetTypes()
        {
            return _reptype.GetAll(); // return 200 (OK)
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public IActionResult GetTypeByID(int id)
        {
            var t = _reptype.Find(id);

            if (t == null)
            {
                return NotFound();
            }

            return new ObjectResult(t); // return 200 (OK)
        }

        // PATCH: api/Types/5
        [HttpPatch("{id}")]
        public IActionResult UpdateTypeDesc([FromBody] Type type, int id)
        {
            if (type ==  null)
            {
                return BadRequest();
            }

            var t = _reptype.Find(id);

            if (t == null)
            {
                return NotFound();
            }

            _reptype.Update(t);

            return NoContent();  //return 204(NO CONTENT)
        }
    }
}
