using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileAPI.Data;
using MobileAPI.Models;

namespace MobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoodschapController : ControllerBase
    {
        private readonly BoodschapContext _context;

        public BoodschapController(BoodschapContext context)
        {
            _context = context;
        }

        // GET: api/Boodschap
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Boodschap>>> GetBoodschappen()
        {
            return await _context.Boodschappen.Include(x=>x.Rows).ToListAsync();
        }

        // GET: api/Boodschap/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Boodschap>> GetBoodschap(int id)
        {
            var boodschap = await _context.Boodschappen.Where(x => x.BoodschapID == id).Include(x=>x.Rows).ThenInclude(y=>y.Product).FirstOrDefaultAsync();

            if (boodschap == null)
            {
                return NotFound();
            }

            return boodschap;
        }
        [HttpGet("user/{userid}")]
        public async Task<ActionResult<Boodschap>> GetOpenBoodschapFromUser(int userid)
        {
            var boodschap = await _context.Boodschappen.Where(x => x.UserID == userid && x.Status=="gepland").Include(x => x.Rows).ThenInclude(y => y.Product).FirstOrDefaultAsync();

            if (boodschap == null)
            {
                return NotFound();
            }

            return boodschap;
        }

        [HttpGet("user/uitgevoerd/{userid}")]
        public async Task<ActionResult<IEnumerable<Boodschap>>> GetAllDoneBoodschappenFromUser(int userid)
        {
            var boodschappen = await _context.Boodschappen.Where(x => x.UserID == userid && x.Status == "uitgevoerd").Include(x => x.Rows).ThenInclude(y => y.Product).ToListAsync();

            if (boodschappen == null || boodschappen.Count == 0)
            {
                return NotFound();
            }

            return boodschappen;
        }

        // PUT: api/Boodschap/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoodschap(int id, Boodschap boodschap)
        {
            if (id != boodschap.BoodschapID)
            {
                return BadRequest();
            }

            _context.Entry(boodschap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoodschapExists(id))
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

        // POST: api/Boodschap
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Boodschap>> PostBoodschap(Boodschap boodschap)
        {
            _context.Boodschappen.Add(boodschap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoodschap", new { id = boodschap.BoodschapID }, boodschap);
        }

        // DELETE: api/Boodschap/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Boodschap>> DeleteBoodschap(int id)
        {
            var boodschap = await _context.Boodschappen.FindAsync(id);
            if (boodschap == null)
            {
                return NotFound();
            }

            _context.Boodschappen.Remove(boodschap);
            await _context.SaveChangesAsync();

            return boodschap;
        }

        private bool BoodschapExists(int id)
        {
            return _context.Boodschappen.Any(e => e.BoodschapID == id);
        }
    }
}
