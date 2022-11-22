using savings_app_backend.Models.Entities;

namespace savings_app_backend.Repositories.Interfaces
{
    public interface IUserAuthRepository
    {
        public Task<UserAuth?> GetUserAuth(Guid id);
        public Task<UserAuth> RemoveUserAuth(UserAuth userAuth);

        public Task<IEnumerable<UserAuth>> GetUserAuths();

        public Task<UserAuth> UpdateUserAuth(UserAuth userAuth);
        public Task<UserAuth> AddUserAuth(UserAuth userAuth);

        public Task<bool> UserAuthExists(Guid id);

    }
}
