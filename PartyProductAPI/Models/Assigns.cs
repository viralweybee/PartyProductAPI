using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyProductAPI.Models
{
    public partial class Assigns
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "PartyId is required")]
        public int PartyId { get; set; }
        [Required(ErrorMessage ="Product Id is required")]
        public int ProductId { get; set; }

        public virtual Parties Party { get; set; }
        public virtual Products Product { get; set; }
    }
}
