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
    public class PickupService : IPickupService
    {
        private readonly IPickupRepository _pickupRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IHttpContextAccessor _httpContext;

        public PickupService(IPickupRepository pickupRepository, IProductRepository productRepository,
            IRestaurantRepository restaurantRepository, IHttpContextAccessor httpContext)
        {
            _pickupRepository = pickupRepository;
            _productRepository = productRepository;
            _restaurantRepository = restaurantRepository;
            _httpContext = httpContext;
        }

        public async Task<Pickup> BookPickup(Guid pickupId)
        {
            var pickup = await GetPickup(pickupId);
            pickup.Status = PickupStatus.Taken;
            await PutPickup(pickupId, pickup);

            return pickup;
        }

        public async Task<Pickup> DeletePickup(Guid id)
        {
            var pickup = await _pickupRepository.GetPickup(id);
            if (pickup == null)
            {
                throw new RecourseNotFoundException();
            }

            var product = await _productRepository.GetProduct(pickup.ProductId);
            var restaurant = await _restaurantRepository.GetRestaurant(product.RestaurantID);

            if (restaurant.Id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();

            await _pickupRepository.RemovePickup(pickup);
            return pickup;
        }

        public async Task<IEnumerable<Pickup>> GetBuyerPickups(Guid buyerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Pickup> GetPickup(Guid id)
        {
            var pickup = await _pickupRepository.GetPickup(id);

            if(pickup == null)
            {
                throw new RecourseNotFoundException();
            }
            else
            {
                return pickup;
            }
        }

        public async Task<IEnumerable<Pickup>> GetPickups()
        {
            return await _pickupRepository.GetPickups();
        }

        public async Task<IEnumerable<Pickup>> GetProductPickups(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Pickup> PostPickup(Pickup pickup)
        {
            pickup.Id = Guid.NewGuid();

            await _pickupRepository.AddPickup(pickup);

            return pickup;
        }

        public async Task<Pickup> PutPickup(Guid id, Pickup pickup)
        {
            if (id != pickup.Id)
            {
                throw new InvalidRequestArgumentsException();
            }

            

            if (!await PickupExistsAsync(id))
            {
                throw new RecourseAlreadyExistsException();
            }

            await _pickupRepository.UpdatePickup(pickup);
            return pickup;
        }


        private async Task<bool> PickupExistsAsync(Guid id)
        {
            return await _pickupRepository.PickupExists(id);
        }
    }
}
