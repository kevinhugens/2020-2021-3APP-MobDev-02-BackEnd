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
    public class BoodschapRowController : ControllerBase
    {
        private readonly BoodschapContext _context;

        public BoodschapRowController(BoodschapContext context)
        {
            _context = context;
        }

        // GET: api/BoodschapRow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoodschapRow>>> GetBoodschapRows()
        {
            return await _context.BoodschapRows.Include(x=>x.Product).ToListAsync();
        }

        // GET: api/BoodschapRow/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoodschapRow>> GetBoodschapRow(int id)
        {
            var boodschapRow = await _context.BoodschapRows.Where(x => x.BoodschapRowID == id).Include(x => x.Product).FirstOrDefaultAsync();

            if (boodschapRow == null)
            {
                return NotFound();
            }

            return boodschapRow;
        }

        // PUT: api/BoodschapRow/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoodschapRow(int id, BoodschapRow boodschapRow)
        {
            if (id != boodschapRow.BoodschapRowID)
            {
                return BadRequest();
            }

            _context.Entry(boodschapRow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoodschapRowExists(id))
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

        // POST: api/BoodschapRow
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BoodschapRow>> PostBoodschapRow(BoodschapRow boodschapRow)
        {
            _context.BoodschapRows.Add(boodschapRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoodschapRow", new { id = boodschapRow.BoodschapRowID }, boodschapRow);
        }

        // DELETE: api/BoodschapRow/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BoodschapRow>> DeleteBoodschapRow(int id)
        {
            var boodschapRow = await _context.BoodschapRows.FindAsync(id);
            if (boodschapRow == null)
            {
                return NotFound();
            }

            _context.BoodschapRows.Remove(boodschapRow);
            await _context.SaveChangesAsync();

            return boodschapRow;
        }

        private bool BoodschapRowExists(int id)
        {
            return _context.BoodschapRows.Any(e => e.BoodschapRowID == id);
        }
    }
}
