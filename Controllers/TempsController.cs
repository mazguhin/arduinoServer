using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArduinoServer.Models;

namespace ArduinoServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Temps")]
    public class TempsController : Controller
    {
        private readonly TestsContext _context;

        public TempsController(TestsContext context)
        {
            _context = context;
        }

        // GET: api/Temps
        [HttpGet]
        public IEnumerable<Temp> GetTemps()
        {
            if (Request.Query.ContainsKey("t") || Request.Query.ContainsKey("h"))
            {
                Temp temp = new Temp();

                if (Request.Query.ContainsKey("d"))
                {
                    temp.Date = Convert.ToDateTime(Request.Query["d"]);
                } else
                {
                    temp.Date = DateTime.Now;
                }

                temp.Temperature = Request.Query["t"];
                temp.Humidity = Request.Query["h"];
                _context.Temps.Add(temp);
                _context.SaveChanges();
                return _context.Temps;
            }
            else
            {
                return _context.Temps;
            }
        }

        // GET: api/Temps/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetTemp([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var temp = await _context.Temps.SingleOrDefaultAsync(m => m.Id == id);

        //    if (temp == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(temp);
        //}

        // PUT: api/Temps/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemp([FromRoute] int id, [FromBody] Temp temp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temp.Id)
            {
                return BadRequest();
            }

            _context.Entry(temp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Temps
        [HttpPost]
        public async Task<IActionResult> PostTemp([FromBody] Temp temp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Temps.Add(temp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemp", new { id = temp.Id }, temp);
        }

        // DELETE: api/Temps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemp([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var temp = await _context.Temps.SingleOrDefaultAsync(m => m.Id == id);
            if (temp == null)
            {
                return NotFound();
            }

            _context.Temps.Remove(temp);
            await _context.SaveChangesAsync();

            return Ok(temp);
        }

        private bool TempExists(int id)
        {
            return _context.Temps.Any(e => e.Id == id);
        }
    }
}