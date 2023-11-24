using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyProductAPI.Models;



namespace PartyProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignsController : ControllerBase
    {
        private readonly PartyProductAPIContext _context;

        public AssignsController(PartyProductAPIContext context)
        {
            _context = context;
        }

        // GET: api/Assigns
        [HttpGet]
        public async Task<ActionResult<List<DisplayAssign>>> GetAssigns()
        {
            var AssginData =await _context.Assigns.Include(x=>x.Party).Include(x=>x.Product).ToListAsync();
            var temp = AssginData.ToLookup(x => x.Id);
            var DisplayAssigns = new List<DisplayAssign>();
            foreach(var item in temp)
            {
                foreach(var itemId in temp[item.Key])
                {
                    DisplayAssigns.Add(new DisplayAssign
                    {
                        id = itemId.Id,
                        partyName=itemId.Party.PName,
                        productName=itemId.Product.PrName
                    });
                }
            }
            return DisplayAssigns;           
        }

        // GET: api/Assigns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assigns>> GetAssigns(int id)
        {
            var assigns = await _context.Assigns.FindAsync(id);

            if (assigns == null)
            {
                return NotFound();
            }

            return assigns;
        }

        // PUT: api/Assigns/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssigns(int id, Assigns assigns)
        {
            if (SameParty_Product(assigns.PartyId, assigns.ProductId))
            {
                return BadRequest("OOps Data is already exists");
            }
            if (id != assigns.Id)
            {
                return BadRequest();
            }

            _context.Entry(assigns).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignsExists(id))
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

        // POST: api/Assigns
        [HttpPost]
        public async Task<ActionResult<Assigns>> PostAssigns(Assigns assigns)
        {
            if (SameParty_Product(assigns.PartyId, assigns.ProductId))
            {
                return BadRequest("OOps Data is already exists");
            }
            _context.Assigns.Add(assigns);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssigns", new { id = assigns.Id }, assigns);
        }

        // DELETE: api/Assigns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Assigns>> DeleteAssigns(int id)
        {
            var assigns = await _context.Assigns.FindAsync(id);
            if (assigns == null)
            {
                return NotFound();
            }

            _context.Assigns.Remove(assigns);
            await _context.SaveChangesAsync();

            return assigns;
        }

        private bool AssignsExists(int id)
        {
            return _context.Assigns.Any(e => e.Id == id);
        }
        public bool SameParty_Product(int partyId,int productId)
        {
            return _context.Assigns.Any(x => x.PartyId == partyId && x.ProductId==productId );
        }
    }
}
