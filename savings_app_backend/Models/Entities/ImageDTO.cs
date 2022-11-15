namespace savings_app_backend.Models.Entities
{
    public class ImageDTO
    {
        public string FileName { get; set; }
        public IFormFile Image { get; set; }

    }
}
