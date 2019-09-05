using DcProcurement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.UtilityMethods
{
    public static class FileUpload
    {
        public static async Task<List<Attachment>> GetFilePathsAsync(List<IFormFile> files, IHostingEnvironment _environment)
        {
            var filePaths = new List< Attachment>();

            var folderPath = Path.Combine(_environment.WebRootPath, "Attachments");
            Directory.CreateDirectory(folderPath);
            foreach (var file in files)
            {               
               
                using (var fileStream = new FileStream(folderPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                filePaths.Add(new Attachment { FilePath =  Path.Combine(folderPath, file.FileName});

            }

            return filePaths;
        }
    }
}
