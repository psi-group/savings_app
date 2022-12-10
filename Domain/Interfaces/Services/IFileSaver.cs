using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
    public interface IFileSaver
    {
        public Task SaveImage(IFormFile imageFile, string fileName, bool isProduct);
    }
}
