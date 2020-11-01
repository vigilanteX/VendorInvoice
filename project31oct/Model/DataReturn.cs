using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project31oct.Model
{
    public class DataReturn
    {

        public int TotalInvoices { get; set; }
        public decimal SumOfAmount { get; set; }
        public int TotalVendors { get; set; }
        public int InvalidInvoiceCount { get; set; }
    }
}
