using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyProductAPI.Models
{
    public partial class Products
    {
        public Products()
        {
            Assigns = new HashSet<Assigns>();
            Invoices = new HashSet<Invoices>();
            ProductRate = new HashSet<ProductRate>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = ("ProductName is Required"))]
        public string PrName { get; set; }

        public virtual ICollection<Assigns> Assigns { get; set; }
        public virtual ICollection<Invoices> Invoices { get; set; }
        public virtual ICollection<ProductRate> ProductRate { get; set; }
    }
}
