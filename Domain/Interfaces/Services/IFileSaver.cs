using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
    public interface IFileSaver
    {
        public Task SaveImage(IFormFile imageFile, string imageName, string path, string extention);
    }
}
