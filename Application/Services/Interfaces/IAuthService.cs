using Domain.DTOs.Request;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IAuthService
    {
        public string Login(UserLoginDTO userLogin);
    }
}
