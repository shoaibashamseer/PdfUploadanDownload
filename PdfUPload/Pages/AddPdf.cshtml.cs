using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PdfUPload.Models;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace PdfUPload.Pages
{
    public class AddPdfModel : PageModel
    {
        private MongoClient _mongoClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<PdfStore> _pdfTable = null;

        public AddPdfModel()
        {
            _mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            _database = _mongoClient.GetDatabase("FileCollections");
            _pdfTable = _database.GetCollection<PdfStore>("PdfStore");
        }
      
        [BindProperty]
        public PdfStore PdfStore { get; set; }
        public int? myID { get; set; }
        [BindProperty]
        public IFormFile File { get; set; }

        [BindProperty]
        public int? ID { get; set; }

        public void OnGet(int? id)
        {
            myID = id;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (File != null)
            {
                if (File.Length > 0 && File.Length < 300000)
                {
                    //var myPdf = _pdfTable.Find(x => x.Id == ID).FirstOrDefault();

                    // var filePath = @"c\:PdfFolder";
                    using (var target = new MemoryStream())
                    {
                       File.CopyTo(target);
                       PdfStore.Attachment = target.ToArray();

                    }

                    await _pdfTable.InsertOneAsync(PdfStore);
                    //  string fileName = Path.GetFileName(file.FileName);
                    // PdfStore.Attachment = target.ToArray();
                    // PdfStore.Attachment = fileName;     

                }
            
            }
            
            return RedirectToPage("./Index");
        }

      
    }
}
