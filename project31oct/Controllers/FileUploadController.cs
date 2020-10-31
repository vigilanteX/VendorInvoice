using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using project31oct.Model;
using project31oct.Repository;

namespace project31oct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        public FileUploadController(IHostingEnvironment hostingEnvironment, ISQLRepository sQLRepository)
        {
            HostingEnvironment = hostingEnvironment;
            SQLRepository = sQLRepository;
        }

        public IHostingEnvironment HostingEnvironment { get; }
        public ISQLRepository SQLRepository { get; }

        public class UploadForm
        {
            public IFormFile FormFile { get; set; }
        }

        [HttpPost]
        [Route("Upload")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync([FromForm] UploadForm upload)
        {
            var fileName = $"{HostingEnvironment.WebRootPath}\\files\\{upload.FormFile.FileName}";
            using (var filestream = System.IO.File.Create(fileName))
            {
                upload.FormFile.CopyTo(filestream);
                filestream.Flush();
            }

            try
            {
                var uploadModel = this.UploadInDB(fileName);

           
            var result = await SQLRepository.UploadDataAsync(uploadModel).ConfigureAwait(false);
            if (!result)
            {
                return BadRequest();
            }
            }
            catch (Exception ex)
            {

                throw;
            }
            return NoContent();

        }

        private IList<UploadModel> UploadInDB(string fName)
        {
            var uploadedModels = new List<UploadModel>();
            //var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // ToDO: change fmname to FileName
            using (var stream = System.IO.File.Open(fName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                { 
                    
                    while (reader.Read())
                    {
                        if (reader.GetValue(0).ToString() == "Invoice Numbers")
                        {
                            continue;
                        }
                        var uploadModel = new UploadModel
                        {
                            InvoiceNumbers = int.TryParse(reader.GetValue(0)?.ToString(), out var invoiceNumber) ? invoiceNumber : 0,
                            DocumentNumber = int.TryParse(reader.GetValue(1)?.ToString(), out var docNumber) ? docNumber : 0,
                            DocumentType= reader.GetValue(2)?.ToString(),
                            NetDueDate= DateTime.TryParse(reader.GetValue(3)?.ToString(), out var netduedate) ? netduedate : new DateTime(),
                            DocumentDate = DateTime.TryParse(reader.GetValue(4)?.ToString(), out var docdate) ? docdate : new DateTime(),
                            PostingDate = DateTime.TryParse(reader.GetValue(5)?.ToString(), out var postingdate) ? postingdate : new DateTime(),
                            Amount=decimal.TryParse(reader.GetValue(6)?.ToString(), out var amnt ) ? amnt : 0,
                            VendorCode= reader.GetValue(7)?.ToString(),
                            VendorName= reader.GetValue(8)?.ToString(),
                            VendorType= reader.GetValue(9)?.ToString(),


                            
                        };

                        uploadedModels.Add(uploadModel);
                    }
                }
            }

            return uploadedModels;
        }



    }
}
