using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppBancaires.Data;
using AppBancaires.Model;

namespace AppBancaires.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComptesController : ControllerBase
    {
        private readonly AppBancairesContext _context;

        public ComptesController(AppBancairesContext context)
        {
            _context = context;
        }

        // GET: api/Comptes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compte>>> GetCompte()
        {
            return await _context.Compte.ToListAsync();
        }

        // GET: api/Comptes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compte>> GetCompte(int id)
        {
            var compte = await _context.Compte.FindAsync(id);

            if (compte == null)
            {
                return NotFound();
            }

            return compte;
        }

        // PUT: api/Comptes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompte(int id, Compte compte)
        {
            if (id != compte.Id)
            {
                return BadRequest();
            }

            _context.Entry(compte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompteExists(id))
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

        // POST: api/Comptes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compte>> PostCompte(Compte compte)
        {
            _context.Compte.Add(compte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompte", new { id = compte.Id }, compte);
        }

        // DELETE: api/Comptes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompte(int id)
        {
            var compte = await _context.Compte.FindAsync(id);
            if (compte == null)
            {
                return NotFound();
            }

            _context.Compte.Remove(compte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompteExists(int id)
        {
            return _context.Compte.Any(e => e.Id == id);
        }
    }
}
