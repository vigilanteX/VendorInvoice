using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project31oct.Model
{
    public class Vendor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorId { get; set; }
        [Required]
        public string VendorCode { get; set; }
        [Required]
        public string VendorName { get; set; }
        [Required]
        public string VendorType { get; set; }

        public IEnumerable<Invoice> Invoices { get; set; }
    }
}
