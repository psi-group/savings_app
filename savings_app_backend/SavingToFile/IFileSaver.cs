namespace savings_app_backend.SavingToFile
{
    public interface IFileSaver
    {
        public Task SaveImage(IFormFile imageFile, string imageName, string path, string extention);
    }
}
