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

namespace BsslProcurement.Pages.Staff
{
    public class PSISSModel : PageModel
    {
        public readonly ProcurementDBContext _context;
        public readonly IWebHostEnvironment _hostingEnvironment;

        public PSISSModel(ProcurementDBContext context, IWebHostEnvironment hostingEnvironment)
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
                string companyCode = "_" + (1).ToString() + "_";
                var fileName = Image.FileName;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "logo");

                //checks if path exists, if not create it.
                Directory.CreateDirectory(uploads);

                var filePath = Path.Combine(uploads, fileName);
                Image.CopyTo(new FileStream(filePath, FileMode.Create));


                return fileName;
            }

            return null;
        }
      



    }
}