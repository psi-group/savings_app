using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.Services.Interfaces
{
    public interface IUserAuthService
    {
        Task<IEnumerable<UserAuth>> GetUserAuths();

        Task<UserAuth> GetUserAuth(Guid id);

        Task<UserAuth> PutUserAuth(Guid id, UserAuth UserAuth);

        Task<UserAuth> DeleteUserAuth(Guid id);

        Task<UserAuth> PostUserAuth(UserAuth UserAuth);

    }
}
