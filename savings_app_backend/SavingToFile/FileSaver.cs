using Microsoft.AspNetCore.Hosting;

namespace savings_app_backend.SavingToFile
{
    public class FileSaver : IFileSaver
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileSaver(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task SaveImage(IFormFile imageFile, string imageName, string path, string extention)
        {
            var imagePath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, path, imageName, extention);

            using (FileStream filestream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(filestream);
            }
            
        }
    }
}
