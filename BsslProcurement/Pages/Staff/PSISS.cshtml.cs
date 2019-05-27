using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DcProcurement;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

namespace BsslProcurement.Pages.Staff
{
    public class PSISSModel : PageModel
    {
        public readonly ProcurementDBContext _context;
        public readonly IHostingEnvironment _hostingEnvironment;

        public PSISSModel(ProcurementDBContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }


        [BindProperty]
        public ProcurementPortalInfo ProcurementPortalInfo { get; set; }

        [BindProperty]
        public IFormFile Image { set; get; }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var imgPath = GetImageFileName();

            if (!ModelState.IsValid || imgPath == null)
            {
               return Page();
            }
            ProcurementPortalInfo.PortalLogo = imgPath;
            _context.ProcurementPortalInfo.Add(ProcurementPortalInfo);

            await _context.SaveChangesAsync();

            return null;

        }

        string GetImageFileName()
        {
            if (Image != null)
            {
                var fileName = GetUniqueName(Image.FileName);
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "logo");
                var filePath = Path.Combine(uploads, fileName);
                Image.CopyTo(new FileStream(filePath, FileMode.Create));


                return filePath;
            }

            return null;
        }
        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }



    }
}