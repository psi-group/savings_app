using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.Repositories.Interfaces;
using savings_app_backend.Services.Interfaces;
using System.Security.Claims;

namespace savings_app_backend.Services.Implementations
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserAuthRepository _userAuthRepository;
        public UserAuthService(IUserAuthRepository userAuthRepository)
        {
            _userAuthRepository = userAuthRepository;
        }

        public async Task<UserAuth> DeleteUserAuth(Guid id)
        {
            
            var userAuth = await _userAuthRepository.GetUserAuth(id);
            if (userAuth == null)
            {
                throw new RecourseNotFoundException();
            }

            //var product = await _context.Products.FindAsync(UserAuth.ProductId);
            //var restaurant = await _context.Restaurants.FindAsync(product.RestaurantID);

            /*if (restaurant.Id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();*/

            await _userAuthRepository.RemoveUserAuth(userAuth);
            return userAuth;
        }

        public Task<IEnumerable<UserAuth>> GetBuyerUserAuths(Guid buyerId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserAuth> GetUserAuth(Guid id)
        {
            var userAuth = await _userAuthRepository.GetUserAuth(id);

            if(userAuth == null)
            {
                throw new RecourseNotFoundException();
            }
            else
            {
                return userAuth;
            }
        }

        public async Task<IEnumerable<UserAuth>> GetUserAuths()
        {
            return await _userAuthRepository.GetUserAuths();
        }

        public async Task<UserAuth> PostUserAuth(UserAuth userAuth)
        {
            userAuth.Id = Guid.NewGuid();

            await _userAuthRepository.AddUserAuth(userAuth);

            return userAuth;
        }

        public async Task<UserAuth> PutUserAuth(Guid id, UserAuth userAuth)
        {
            if (id != userAuth.Id)
            {
                throw new InvalidRequestArgumentsException();
            }

            if (!await UserAuthExistsAsync(id))
            {
                throw new RecourseNotFoundException();
            }

            await _userAuthRepository.UpdateUserAuth(userAuth);
            return userAuth;
        }

        private async Task<bool> UserAuthExistsAsync(Guid id)
        {
            return await _userAuthRepository.UserAuthExists(id);
        }
    }
}
