using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services.Implementations
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
            throw new NotImplementedException();
            /*var pickup = await GetPickup(pickupId);
            pickup.Book();
            pickup.Status = PickupStatus.Taken;
            await PutPickup(pickupId, pickup);

            return pickup;*/
        }

        public async Task<Pickup> DeletePickup(Guid id)
        {
            var pickup = await _pickupRepository.GetPickupAsync(id);
            if (pickup == null)
            {
                throw new RecourseNotFoundException();
            }

            var product = await _productRepository.GetProductAsync(pickup.ProductId);
            var restaurant = await _restaurantRepository.GetRestaurantAsync(product.RestaurantID);

            if (restaurant.Id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();

            _pickupRepository.RemovePickup(pickup);
            await _pickupRepository.SaveChangesAsync();
            return pickup;
        }

        public async Task<IEnumerable<Pickup>> GetBuyerPickups(Guid buyerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Pickup> GetPickup(Guid id)
        {
            var pickup = await _pickupRepository.GetPickupAsync(id);

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
            return await _pickupRepository.GetPickupsAsync();
        }

        public async Task<IEnumerable<Pickup>> GetProductPickups(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Pickup> PostPickup(Pickup pickup)
        {
            pickup.GenerateId();
            //pickup.Id = Guid.NewGuid();

            await _pickupRepository.AddPickupAsync(pickup);
            await _pickupRepository.SaveChangesAsync();

            return pickup;
        }

        public async Task<Pickup> PutPickup(Guid id, Pickup pickup)
        {
            if (id != pickup.Id)
            {
                throw new InvalidRequestArgumentsException();
            }

            

            if (!await _pickupRepository.PickupExistsAsync(id))
            {
                throw new RecourseAlreadyExistsException();
            }

            _pickupRepository.UpdatePickup(pickup);
            await _pickupRepository.SaveChangesAsync();
            return pickup;
        }

    }
}
