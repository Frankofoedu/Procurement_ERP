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
        public static async Task<List<Attachment>> GetFilePathsFromMultipleFileListAsync(List<IFormFile> files, IWebHostEnvironment _environment, string folder)
        {
            if (files == null)
            {
                return null;
            }
            var filePaths = new List< Attachment>();

            var folderPath = Path.Combine(_environment.WebRootPath, folder);
            Directory.CreateDirectory(folderPath);
            try
            {

                foreach (var file in files)
                {
                    var filename = DateTime.Now.Ticks.ToString() + file.FileName;

                    using (var fileStream = new FileStream(Path.Combine(folderPath, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    filePaths.Add(new Attachment { FilePath = filename });
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("File not found");
            }
            catch (Exception ex)
            {

                throw new Exception("An error ocurred.  " + ex.Message);
            }
            return filePaths;
        }
        public static async Task<Attachment> GetFilePathsFromFileAsync(IFormFile file, IWebHostEnvironment _environment, string folder)
        {
            if (file == null)
            {
                return null;
            }

            var folderPath = Path.Combine(_environment.WebRootPath, folder);
            Directory.CreateDirectory(folderPath);

            var filename = DateTime.Now.Ticks.ToString() + file.FileName;
            try
            {               

                using var fileStream = new FileStream(Path.Combine(folderPath, filename), FileMode.Create);

                await file.CopyToAsync(fileStream);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("File not found");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured" + ex.Message);
            }

            
            return new Attachment { FilePath = Path.Combine(folder, filename) ,DateCreated = DateTime.Now};
        }
    }
}
