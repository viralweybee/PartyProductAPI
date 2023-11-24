using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PartyProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PartyProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductRateController:ControllerBase
    {
        private readonly PartyProductAPIContext context;
        public ProductRateController(PartyProductAPIContext context)
        {
            this.context = context;
        }
        //Get : api/ProductRate
        [HttpGet]
        public async Task<ActionResult<List<DisplayProductRate>>> GetProduct()
        {
            var productDetails = new List<DisplayProductRate>();
            var getProducts =await context.ProductRate.Include(a => a.Pr).ToListAsync();
            var mappingProduct = getProducts.ToLookup(x => x.Id);
            foreach(var item in mappingProduct)
            {
                foreach(var item1 in mappingProduct[item.Key]){
                  
                    productDetails.Add(new DisplayProductRate
                    {
                        Id = item1.Id,
                        productName = item1.Pr.PrName,
                        Rate = item1.Rate,
                        DateOfPurchase = item1.Date
                    });
                }
            }
            return productDetails;
        }
        //Get : api/ProductRate/2
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductRate>> GetProduct(int Id)
        {
            if (!context.ProductRate.Any(x => x.Id == Id))
            {
                return NotFound();
            }
            var productRate = await context.ProductRate.FindAsync(Id);
            return productRate;
        }
        //Delete : api/ProductRate/3
        [HttpDelete("{id}")]
       public async Task<ActionResult<ProductRate>> DeleteProduct(int id)
        {
            var findId =await context.ProductRate.FindAsync(id);
            if (!checkExists(id))
            {
                return NotFound();
            }
            context.ProductRate.Remove(findId);
            await context.SaveChangesAsync();
            return findId;
        }
        //Put : api/ProductRate/3
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,ProductRate productRate)
        {
            if (checkProductId(productRate.PrId, productRate.Rate, productRate.Date)){
                return BadRequest("OOps data is already exists same as kindly check it!");
            }
            if (!checkExists(id))
            {
                return NotFound();
            }
            
            context.Entry(productRate).State = EntityState.Modified;
            await context.SaveChangesAsync();
           
            return NoContent();

        }
        //Post : api/ProductRate
        [HttpPost]
        public async Task<ActionResult<ProductRate>> AddProduct(ProductRate productRate)

        {
            if (checkProductId(productRate.PrId, productRate.Rate, productRate.Date))
            {
                return BadRequest("OOps data is already exists same as kindly check it!");
            }
            context.ProductRate.Add(productRate);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = productRate.Id }, productRate);

        }
        public bool checkExists(int id)
        {
            var checking = context.ProductRate.Any(x => x.Id == id);
            return checking;
        }
        public bool checkProductId(int prId,int rate,DateTime date)
        {
            return context.ProductRate.Any(x => x.PrId == prId && x.Rate == rate && x.Date == date);
        }
    }
}
