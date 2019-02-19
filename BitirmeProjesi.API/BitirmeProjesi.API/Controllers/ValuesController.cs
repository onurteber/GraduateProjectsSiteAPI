using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitirmeProjesi.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> GetStudent()
        {
            var model = await _context.Universities.ToListAsync();
            return Ok(model);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudent(int id)
        {
            var model = await _context.Universities.FirstOrDefaultAsync(s=>s.UniversityID==id);
            return Ok(model);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
