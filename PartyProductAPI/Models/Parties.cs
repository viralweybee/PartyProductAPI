using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyProductAPI.Models
{
    public partial class Parties
    {
        public Parties()
        {
            Assigns = new HashSet<Assigns>();
            Invoices = new HashSet<Invoices>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage =("PartyName is Required"))]   
        public string PName { get; set; }
        public virtual ICollection<Assigns> Assigns { get; set; }
        public virtual ICollection<Invoices> Invoices { get; set; }
    }
}
