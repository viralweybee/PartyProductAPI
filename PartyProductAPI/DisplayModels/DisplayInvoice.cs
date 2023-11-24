using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyProductAPI.DisplayModels
{
    public class DisplayInvoice
    {
        public int Id { get; set; }
        public string PartyName { get; set; }
        public string ProductName { get; set; }
        public int Rate { get; set; }
        public int quantity { get; set; }
        public int total { get; set; }
    }
}
