using Domain.DTOs.Request;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> Login(UserLoginDTO userLogin);
    }
}
