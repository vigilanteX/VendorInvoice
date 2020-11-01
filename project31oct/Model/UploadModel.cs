using Microsoft.AspNetCore.Razor.Language;
using project31oct.CustomValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project31oct.Model
{
    public class UploadModel
    {
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
        [ValidDate]
        public DateTime PostingDate { get; set; }//not be future date

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string VendorCode { get; set; }
        [Required]
        public string VendorName { get; set; }
        [Required]
        public string VendorType { get; set; }

        public string Errors { get; set; }
    }
}
