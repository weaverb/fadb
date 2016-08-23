using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fadb_api.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace fadb_api.Controllers
{
    [Route("api/[controller]")]
    public class FirearmController : Controller
    {
        public FirearmController(IFirearmRepository firearms)
        {
            Firearms = firearms;
        }

        [HttpGet]
        public IEnumerable<Firearm> GetAll()
        {
            return Firearms.GetAll();
        }

        [HttpGet("{id}", Name ="GetFirearm")]
        public IActionResult GetById(string id)
        {
            var firearm = Firearms.Find(id);
            if(firearm == null)
            {
                return NotFound();
            }

            return new ObjectResult(firearm);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Firearm firearm)
        {
            if(firearm == null)
            {
                return BadRequest();
            }

            Firearms.Add(firearm);
            return CreatedAtRoute("GetFirearm", new { id = firearm.Key }, firearm);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Firearm firearm)
        {
            if(firearm == null || firearm.Key != id)
            {
                return BadRequest();
            }

            var oldFirearm = Firearms.Find(id);
            if(oldFirearm == null)
            {
                return NotFound();
            }

            Firearms.Update(firearm);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var firearm = Firearms.Find(id);
            if(firearm == null)
            {
                return NotFound();
            }
            Firearms.Remove(id);
            return new NoContentResult();
        }

        public IFirearmRepository Firearms { get; set; }
    }
}
