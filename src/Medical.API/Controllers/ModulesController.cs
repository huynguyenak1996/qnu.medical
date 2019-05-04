using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Medical.Data;
using Medical.Entities.System;

namespace Medical.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModulesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Modules
        [HttpGet]
        [Route("getModules")]
        public IEnumerable<vnc_Modules> GetModules()
        {
            return _context.VNC_Modules;
        }

        // GET: api/Modules/5
        [HttpGet]
        [Route("getModulesById/{id}")]
        public async Task<IActionResult> GetModulesById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vnc_Modules = await _context.VNC_Modules.FindAsync(id);

            if (vnc_Modules == null)
            {
                return NotFound();
            }

            return Ok(vnc_Modules);
        }

        // PUT: api/Modules/5
        [HttpPut]
        [Route("putModules")]
        public async Task<IActionResult> PutModules([FromRoute] int id, [FromBody] vnc_Modules vnc_Modules)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vnc_Modules.Id)
            {
                return BadRequest();
            }

            _context.Entry(vnc_Modules).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModulesExists(id))
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

        // POST: api/Modules
        [HttpPost]
        [Route("postModules")]
        public async Task<IActionResult> PostModules([FromBody] vnc_Modules vnc_Modules)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VNC_Modules.Add(vnc_Modules);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getvnc_Modules", new { id = vnc_Modules.Id }, vnc_Modules);
        }

        // DELETE: api/Modules/5
        [HttpDelete]
        [Route("deleteModules/{id}")]
        public async Task<IActionResult> DeleteModules([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vnc_Modules = await _context.VNC_Modules.FindAsync(id);
            if (vnc_Modules == null)
            {
                return NotFound();
            }

            _context.VNC_Modules.Remove(vnc_Modules);
            await _context.SaveChangesAsync();

            return Ok(vnc_Modules);
        }

        private bool ModulesExists(int id)
        {
            return _context.VNC_Modules.Any(e => e.Id == id);
        }
    }
}