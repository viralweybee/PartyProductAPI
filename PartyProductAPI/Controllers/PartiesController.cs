using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyProductAPI.DisplayModels;
using PartyProductAPI.Models;

namespace PartyProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartiesController : ControllerBase
    {
        private readonly PartyProductAPIContext _context;

        public PartiesController(PartyProductAPIContext context)
        {
            _context = context;
        }

        // GET: api/Parties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisplayParty>>> GetParties()
        {
            var partyData =await _context.Parties.ToListAsync();
            var partyDetails = new List<DisplayParty>();
            for(int i = 0; i < partyData.Count(); i++)
            {
                partyDetails.Add(new DisplayParty
                {
                    Id = partyData[i].Id,
                    PartyName = partyData[i].PName
                });
            }
            return partyDetails;
        }

        // GET: api/Parties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parties>> GetParties(int id)
        {
            var parties = await _context.Parties.FindAsync(id);

            if (parties == null)
            {
                return NotFound();
            }

            return parties;
        }

        // PUT: api/Parties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParties(int id, Parties parties)
        {
            if (id != parties.Id)
            {
                return BadRequest();
            }
            if (PartyName(parties.PName))
            {
                return BadRequest("OOPs party is already exists");
            }
            _context.Entry(parties).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartiesExists(id))
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

        // POST: api/Parties  
        [HttpPost]
        public async Task<ActionResult<Parties>> PostParties(Parties parties)
        {
            if (PartyName(parties.PName))
            {
                return BadRequest("PartyName Already exists");
            }
            _context.Parties.Add(parties);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParties", new { id = parties.Id }, parties);
        }

        // DELETE: api/Parties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Parties>> DeleteParties(int id)
        {
            var parties = await _context.Parties.FindAsync(id);
            if (parties == null)
            {
                return NotFound();
            }

            _context.Parties.Remove(parties);
            await _context.SaveChangesAsync();

            return parties;
        }

        private bool PartiesExists(int id)
        {
            return _context.Parties.Any(e => e.Id == id);
        }
        private bool PartyName(string name)
        {
            return _context.Parties.Any(e => e.PName == name);
        }
    }
}
