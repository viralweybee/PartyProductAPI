using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyProductAPI.Models;
using PartyProductAPI.DisplayModels;

namespace PartyProductAPI.Controllers
{
    [Route("api/Invoice")]
    [ApiController]
    public class InvoiceController:ControllerBase
    {
        private readonly PartyProductAPIContext context;
        public InvoiceController(PartyProductAPIContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<DisplayInvoice>>> GetResult()
        {
            var inoviceData = await context.Invoices.FromSqlRaw("GetAllInvoices").ToListAsync(); 
            
            var displayInvoice = new List<DisplayInvoice>();
            for (int i = 0; i < inoviceData.Count(); i++)
            {
              
                int sum = inoviceData[i].Rate * inoviceData[i].Quantity;
                displayInvoice.Add(new DisplayInvoice
                {
                    Id = inoviceData[i].Id,
                    PartyName = context.Parties.Find(inoviceData[i].PartyId).PName,
                    ProductName = context.Products.Find(inoviceData[i].ProductId).PrName,
                    Rate = inoviceData[i].Rate,
                    quantity = inoviceData[i].Quantity,
                    total = sum
                });
            }
            return displayInvoice;
        }
        [HttpPost]
        public async Task<ActionResult> PostResult(Invoices invoice)
        {
            context.Invoices.Add(invoice);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            context.Invoices.RemoveRange(context.Invoices);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("{search}")]
        public async Task<ActionResult<List<DisplayInvoice>>> Search([FromQuery] string productName)
        {
            var fillData = await context.Invoices.FromSqlRaw("GetAllInvoices").ToListAsync();
            var displaysearch = new List<DisplayInvoice>();
            bool flag = false;
            for(int i = 0; i < fillData.Count(); i++)
            {
                string productName1 = context.Products.Find(fillData[i].ProductId).PrName;
                if (productName1.Contains(productName))
                {
                    flag = true;
                    displaysearch.Add(new DisplayInvoice
                    {
                        Id = fillData[i].Id,
                        PartyName = context.Parties.Find(fillData[i].PartyId).PName,
                        ProductName = context.Products.Find(fillData[i].ProductId).PrName,
                        Rate = fillData[i].Rate,
                        quantity = fillData[i].Quantity,
                        total = fillData[i].Rate * fillData[i].Quantity
                    });
                }
            }
            if (!flag)
            {
                return NoContent();   
            }
            return Ok(displaysearch);
        }
    }
}
