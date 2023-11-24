using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyProductAPI.Models
{
    public partial class Invoices
    {
        public int Id { get; set; }
        [Required]
        public int? PartyId { get; set; }
        [Required]
        public int? ProductId { get; set; }
        [Required]
        public int Rate { get; set; }
        [Required]
        public int Quantity { get; set; }

        public virtual Parties Party { get; set; }
        public virtual Products Product { get; set; }
    }
}
