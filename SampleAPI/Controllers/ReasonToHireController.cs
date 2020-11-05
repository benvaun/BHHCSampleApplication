using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReasonToHireController : ControllerBase
    {
        private readonly ReasonToHireContext _context;

        public ReasonToHireController(ReasonToHireContext context)
        {
            _context = context;
        }

        // GET: api/ReasonToHire
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReasonToHire>>> GetReasonsToHire()
        {
            return await _context.ReasonsToHire.ToListAsync();
        }

        // GET: api/ReasonToHire/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReasonToHire>> GetReasonToHire(long id)
        {
            var reasonToHire = await _context.ReasonsToHire.FindAsync(id);

            if (reasonToHire == null)
            {
                return NotFound();
            }

            return reasonToHire;
        }

        // PUT: api/ReasonToHire/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReasonToHire(long id, ReasonToHire reasonToHire)
        {
            if (id != reasonToHire.Id)
            {
                return BadRequest();
            }

            _context.Entry(reasonToHire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReasonToHireExists(id))
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

        // POST: api/ReasonToHire
        [HttpPost]
        public async Task<ActionResult<ReasonToHire>> PostReasonToHire(ReasonToHire reasonToHire)
        {
            _context.ReasonsToHire.Add(reasonToHire);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReasonToHire), new { id = reasonToHire.Id }, reasonToHire);
        }

        // DELETE: api/ReasonToHire/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReasonToHire>> DeleteReasonToHire(long id)
        {
            var reasonToHire = await _context.ReasonsToHire.FindAsync(id);
            if (reasonToHire == null)
            {
                return NotFound();
            }

            _context.ReasonsToHire.Remove(reasonToHire);
            await _context.SaveChangesAsync();

            return reasonToHire;
        }

        private bool ReasonToHireExists(long id)
        {
            return _context.ReasonsToHire.Any(e => e.Id == id);
        }
    }
}
