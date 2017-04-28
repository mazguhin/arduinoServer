using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ArduinoServer.Models;

namespace ArduinoServer.Controllers
{
    [Route("api/[controller]")]
    public class TestsController : Controller
    {
        TestsContext db;
        public TestsController(TestsContext context)
        {
            this.db = context;
        }

        //[HttpGet]
        //public IEnumerable<Test> Get()
        //{
        //    return db.Tests.ToList();
        //}

        [HttpGet]
        public string Get()
        {
            return "YEAAAA";
        }

        // GET api/tests/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Test test = db.Tests.FirstOrDefault(x => x.Id == id);
            if (test == null)
                return NotFound();
            return new ObjectResult(test);
        }

        // POST api/tests
        [HttpPost]
        public IActionResult Post([FromBody]Test test)
        {
            if (test == null)
            {
                return Content("400");
                //return BadRequest();
            }

            db.Tests.Add(test);
            db.SaveChanges();
            return Content("200");
            //return Ok(test);
        }

        // PUT api/tests/
        [HttpPut]
        public IActionResult Put([FromBody]Test test)
        {
            if (test == null)
            {
                return BadRequest();
            }
            if (!db.Tests.Any(x => x.Id == test.Id))
            {
                return NotFound();
            }

            db.Update(test);
            db.SaveChanges();
            return Ok(test);
        }

        // DELETE api/tests/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Test test = db.Tests.FirstOrDefault(x => x.Id == id);
            if (test == null)
            {
                return NotFound();
            }
            db.Tests.Remove(test);
            db.SaveChanges();
            return Ok(test);
        }
    }
}