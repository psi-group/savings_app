using Application.Services.Interfaces;
using Application.Specifications;
using Domain.DTOs.Request;
using Domain.Entities;
using Domain.Enums;
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
            var pickup = await GetPickup(pickupId);
            pickup.Book();
            await PutPickup(pickupId, pickup);

            return pickup;
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
            var spec = new BuyerPickupSpecification(buyerId);

            return await _pickupRepository.GetPickupsAsync(spec);
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
            var spec = new ProductPickupSpecification(productId);

            return await _pickupRepository.GetPickupsAsync(spec);
        }

        public async Task<Pickup> PostPickup(PickupDTORequest pickupToPost)
        {
            var pickup = new Pickup(
                Guid.NewGuid(),
                (Guid)pickupToPost.ProductId!,
                (DateTime)pickupToPost.StartTime!,
                (DateTime)pickupToPost.EndTime!,
                (PickupStatus)pickupToPost.Status!
                );

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
