using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyProductAPI.Models
{
    public partial class ProductRate
    {
            
        public int Id { get; set; }
        [Required (ErrorMessage="Product Id is Required")]
        public int PrId { get; set; }
        [Required(ErrorMessage = "Rate is Required")]
        public int Rate { get; set; }
        [Required(ErrorMessage = "Date is Required")]
        public DateTime Date { get; set; }

        public virtual Products Pr { get; set; }
    }
}
