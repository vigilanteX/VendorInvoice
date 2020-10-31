using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project31oct.Model
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }

        [Required]
        public int InvoiceNumbers { get; set; }
        [Required]
        public int DocumentNumber { get; set; }
        [Required]
        public string DocumentType { get; set; }
        [Required]
        public DateTime NetDueDate { get; set; }
        [Required]
        public DateTime DocumentDate { get; set; }
        [Required]
        public DateTime PostingDate { get; set; }//not be future date

        [Required]
        public decimal Amount { get; set; }

        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
