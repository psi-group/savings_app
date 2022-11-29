using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class FileSaver : IFileSaver
    {
        public async Task SaveImage(IFormFile imageFile, string imageName, string path, string extention)
        {
            var imagePath = System.IO.Path.Combine(path, imageName, extention);

            using (FileStream filestream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(filestream);
            }
        }
    }
}
