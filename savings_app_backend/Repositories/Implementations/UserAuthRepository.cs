using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Repositories.Interfaces;

namespace savings_app_backend.Repositories.Implementations
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly SavingsAppContext _appContext;

        public UserAuthRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<UserAuth> AddUserAuth(UserAuth userAuth)
        {
            _appContext.UserAuths.Add(userAuth);

            await _appContext.SaveChangesAsync();

            return userAuth;
        }

        public async Task<UserAuth?> GetUserAuth(Guid id)
        {
            return await _appContext.UserAuths.FindAsync();
        }

        public async Task<IEnumerable<UserAuth>> GetUserAuths()
        {
            return await _appContext.UserAuths.ToListAsync();
        }

        public async Task<bool> UserAuthExists(Guid id)
        {
            return await _appContext.UserAuths.AnyAsync(e => e.Id == id);
        }

        public async Task<UserAuth> RemoveUserAuth(UserAuth userAuth)
        {

            _appContext.UserAuths.Remove(userAuth);
            await _appContext.SaveChangesAsync();
            return userAuth;
            
        }

        public async Task<UserAuth> UpdateUserAuth(UserAuth userAuth)
        {
            _appContext.Entry(userAuth).State = EntityState.Modified;

            await _appContext.SaveChangesAsync();

            return userAuth;
        }
    }
}
