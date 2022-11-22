using savings_app_backend.Models.Entities;

namespace savings_app_backend.Services.Interfaces
{
    public interface IAuthService
    {
        public string GenerateToken(User user);
    }
}
