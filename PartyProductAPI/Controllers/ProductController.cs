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
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly PartyProductAPIContext _context;
        public ProductController(PartyProductAPIContext context)
        {
            _context = context;
        }
        //Get : api/Product
        [HttpGet]
        public async Task<ActionResult<List<DisplayProduct>>> GetProducts()
        {
            //Get method will show all the details of products
            var productDetails = await _context.Products.ToListAsync();
            var displayProduct = new List<DisplayProduct>();
            for(int i = 0; i < productDetails.Count(); i++)
            {
                displayProduct.Add(new DisplayProduct
                {
                    Id = productDetails[i].Id,
                    ProductName=productDetails[i].PrName
                });
            }
            return Ok(displayProduct);
        }
        //Get : api/Product/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var exitsParty = _context.Products.Any(x => x.Id == id);
            if (product == null || !exitsParty)
            {
                return NotFound();
            }
            return product;
        }
        //Post : api/Product
        [HttpPost]
        public async Task<ActionResult<Products>> AddProduct(Products products)
        {
            if (ProductName(products.PrName))
            {
                return BadRequest("OOps party already exits");
            }
            _context.Products.Add(products);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProducts", new { id = products.Id }, products);
        }
       //Put: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (id != products.Id)
            {
                return BadRequest();
            }
            if (ProductName(products.PrName))
            {
                return BadRequest("OOps party already exits");
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(x=>x.Id==id))
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
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return products;
        }
        public bool ProductName(string Name)
        {
            return _context.Products.Any(x => x.PrName == Name);
        }
    }
}
