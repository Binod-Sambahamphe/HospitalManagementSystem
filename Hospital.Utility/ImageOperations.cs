using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hospital.Utility
{
    public class ImageOperations
    {
        private readonly IWebHostEnvironment _env;

        public ImageOperations(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> ImageUploadAsync(IFormFile file)
        {
            string filename = null;
            if (file != null && file.Length > 0)
            {
                // Ensure the Images directory exists
                string fileDirectory = Path.Combine(_env.WebRootPath, "Images");
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                // Generate a unique file name
                filename = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                string filepath = Path.Combine(fileDirectory, filename);

                // Save the file asynchronously
                using (FileStream fs = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
            }
            return filename;
        }
    }
}
