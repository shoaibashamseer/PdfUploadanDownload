using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using PdfUPload.Models;
using System.Collections.Generic;
using System.IO;
/*using System.Linq;
using System.Threading.Tasks;

namespace PdfUpload.Pages
{
    public class ShowPdfModel : PageModel
    {
        public IFormFile File { get; set; }

        public async Task<IActionResult> OnShowAsync()
        {

            string fileName = Path.GetFileName(File.FileName);
            FileStream file = new FileStream(fileName, FileMode.Append, FileAccess.Read, FileShare.Read)
            {


            };
        }
    }
}*/