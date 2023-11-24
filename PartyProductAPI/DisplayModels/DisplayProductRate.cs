using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyProductAPI.Models
{
    public class DisplayProductRate
    {
        public int Id { get; set; }
        public string productName{get;set;}
        public int Rate { get; set; }
        public DateTime DateOfPurchase { get; set; }
    }
}
