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
        /// <summary>
        /// Get File paths of attached folder
        /// </summary>
        /// <param name="files">list of files to be uploaded</param>
        /// <param name="_environment"></param>
        /// <param name="folder">storage folder</param>
        /// <returns></returns>
        public static async Task<List<Attachment>> GetFilePathsAsync(List<IFormFile> files, IHostingEnvironment _environment, string folder)
        {
            var filePaths = new List< Attachment>();

            var folderPath = Path.Combine(_environment.WebRootPath, folder);
            Directory.CreateDirectory(folderPath);
            foreach (var file in files)
            {               
               
                using (var fileStream = new FileStream(Path.Combine( folderPath,file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                filePaths.Add(new Attachment { FilePath =  file.FileName});

            }

            return filePaths;
        }
    }
}
