using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using PdfUPload.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfUpload.Pages
{
    public class IndexModel : PageModel
    {
        private MongoClient _mongoClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<PdfStore> _pdfTable = null;

        public IndexModel()
        {
            _mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            _database = _mongoClient.GetDatabase("FileCollections");
            _pdfTable = _database.GetCollection<PdfStore>("PdfStore");
        }
        public IList<PdfStore> PdfStore { get; set; }

        [BindProperty]
        public IFormFile File { get; set; }

        public void OnGet()
        {
            PdfStore = _pdfTable.Find(FilterDefinition<PdfStore>.Empty).ToList();

        }
       
        public async Task<IActionResult> OnPostDownloadAsync(int? id)
        {
            var myPdf = _pdfTable.Find(x => x.Id == id).FirstOrDefault();
            if (myPdf == null)
            {
                return NotFound();
            }
            if (myPdf.Attachment == null)
            {
                return Page();

            }
            else
            {
                byte[] byteArr = myPdf.Attachment;
                string mimeType = "application/pdf";
                return new FileContentResult(byteArr, mimeType)
                {
                    FileDownloadName = $"{myPdf.Name}.pdf"

                };

            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            await _pdfTable.DeleteOneAsync(x => x.Id == id);
            return RedirectToPage("./Index");
    
        }
    }
}