using project31oct.Data;
using project31oct.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project31oct.Repository
{
    public class SQLRepository : ISQLRepository
    {
        public SQLRepository(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public DataContext DataContext { get; }

        public async Task<bool> UploadDataAsync(IList<UploadModel> uploadModels)
        {
            foreach (var model in uploadModels)
            {
                var isInvoiceExists = DataContext.Invoices.Any(x => x.InvoiceNumbers == model.InvoiceNumbers);

                // if invoice does not exist
                if (!isInvoiceExists)
                {
                    var vendorModel = DataContext.Vendors.FirstOrDefault(x => x.VendorCode == model.VendorCode);
                    var vendorId = vendorModel?.VendorId;
                    if (vendorModel == null)
                    {
                        var newVedor = new Vendor
                        {
                            VendorCode = model.VendorCode,
                            VendorName = model.VendorName,
                            VendorType = model.VendorType
                        };

                        DataContext.Vendors.Add(newVedor);
                        vendorId = newVedor.VendorId;
                    }
                    var newInvoice = new Invoice
                    {
                        VendorId = vendorId.Value,
                        Amount = model.Amount,
                        DocumentDate = model.DocumentDate,
                        DocumentNumber = model.DocumentNumber,
                        DocumentType = model.DocumentType,
                        InvoiceNumbers = model.InvoiceNumbers,
                        NetDueDate = model.NetDueDate,
                        PostingDate = model.PostingDate,
                    };

                    DataContext.Invoices.Add(newInvoice).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    //return true;
                }
                await DataContext.SaveChangesAsync().ConfigureAwait(false);

            }
            return true;
        }
    }
}
