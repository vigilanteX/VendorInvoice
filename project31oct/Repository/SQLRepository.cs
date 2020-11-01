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

        public async Task<DataReturn> UploadDataAsync(IList<UploadModel> uploadModels)
        {
            var initialVendorCount = DataContext.Vendors.Count();
            var finalVendorCount = 0;
            var initialInvoiceCount = DataContext.Invoices.Count();
            var finalInvoiceCount = 0;
            var totalAmount = 0.00m;
            var invalidCount = 0;
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
                    totalAmount += newInvoice.Amount;
                    //return true;
                }
                else 
                {
                    invalidCount ++;
                }
                await DataContext.SaveChangesAsync().ConfigureAwait(false);

            }

            finalInvoiceCount = DataContext.Invoices.Count();
            finalVendorCount = DataContext.Vendors.Count();

            var returnModel = new DataReturn
            {
                TotalInvoices = finalInvoiceCount - initialInvoiceCount,
                TotalVendors = finalVendorCount - initialVendorCount,
                SumOfAmount = totalAmount,
                InvalidInvoiceCount = invalidCount
            };
            return returnModel;
        }
    }
}