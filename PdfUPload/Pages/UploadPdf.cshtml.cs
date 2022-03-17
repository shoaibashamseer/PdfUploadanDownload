using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using PdfUPload.Models;

namespace PdfUpload.Pages
{
    public class UploadPdfModel : PageModel
    {
        private MongoClient _mongoClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<PdfStore> _pdfTable = null;

        public UploadPdfModel()
        {
            _mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            _database = _mongoClient.GetDatabase("FileCollections");
            _pdfTable = _database.GetCollection<PdfStore>("PdfStore");
        }
        public int? myID { get; set; }
        [BindProperty]
        public IFormFile file { get; set; }

        [BindProperty]
        public int? ID { get; set; }
        public void OnGet(int? id)
        {
            myID = id;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (file != null)
            {
                if (file.Length > 0 && file.Length < 300000)
                {
                    var myPdf = _pdfTable.Find(x => x.Id == ID).FirstOrDefault();
                    using (var target = new MemoryStream())
                    {
                        file.CopyTo(target);
                        myPdf.Attachment = target.ToArray();
                        
                    }
                    await _pdfTable.ReplaceOneAsync(x => x.Id == ID, myPdf);
                }
            }
            return RedirectToPage("./Index");
        }
    }
}

