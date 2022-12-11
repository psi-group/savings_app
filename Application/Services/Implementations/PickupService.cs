using Application.Services.Interfaces;
using Application.Specifications;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
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

        public async Task<PickupDTOResponse> DeletePickup(Guid id)
        {
            var pickup = await _pickupRepository.GetPickupAsync(id);
            if (pickup == null)
            {
                throw new RecourseNotFoundException("pickup with this id does not exist");
            }

            var product = await _productRepository.GetProductAsync(pickup.ProductId);
            var restaurant = await _restaurantRepository.GetRestaurantAsync(product.RestaurantID);

            if (restaurant.Id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException("you are unauthorized to delete this resource");

            _pickupRepository.RemovePickup(pickup);
            await _pickupRepository.SaveChangesAsync();

            var pickupResponse = new PickupDTOResponse(
                    pickup.Id,
                    pickup.ProductId,
                    pickup.StartTime,
                    pickup.EndTime,
                    pickup.Status
                    );

            return pickupResponse;
        }

        public async Task<IEnumerable<PickupDTOResponse>> GetBuyerPickups(Guid buyerId)
        {
            if (buyerId !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException("you are unauthorized to access this resource");

            var spec = new BuyerPickupSpecification(buyerId);

            var pickups = await _pickupRepository.GetPickupsAsync(spec);

            var pickupsResponse = new List<PickupDTOResponse>();
            foreach (var pickup in pickups)
            {
                var pickupResponse = new PickupDTOResponse(
                pickup.Id,
                pickup.ProductId,
                pickup.StartTime,
                pickup.EndTime,
                pickup.Status
                );
                pickupsResponse.Add(pickupResponse);
            }
            return pickupsResponse;
        }

        public async Task<PickupDTOResponse> GetPickup(Guid id)
        {
            var pickup = await _pickupRepository.GetPickupAsync(id);

            if(pickup == null)
            {
                throw new RecourseNotFoundException("pickup with this id does not exist");
            }
            else
            {
                var pickupResponse = new PickupDTOResponse(
                    pickup.Id,
                    pickup.ProductId,
                    pickup.StartTime,
                    pickup.EndTime,
                    pickup.Status
                    );
                return pickupResponse;
            }
        }

        public async Task<IEnumerable<PickupDTOResponse>> GetPickups()
        {
            var pickups =  await _pickupRepository.GetPickupsAsync();

            var pickupsResponse = new List<PickupDTOResponse>();
            foreach (var pickup in pickups)
            {
                var pickupResponse = new PickupDTOResponse(
                pickup.Id,
                pickup.ProductId,
                pickup.StartTime,
                pickup.EndTime,
                pickup.Status
                );
                pickupsResponse.Add(pickupResponse);
            }
            return pickupsResponse;
        }

        public async Task<IEnumerable<PickupDTOResponse>> GetProductPickups(Guid productId)
        {
            var spec = new ProductPickupSpecification(productId);

            var pickups = await _pickupRepository.GetPickupsAsync(spec);

            var pickupsResponse = new List<PickupDTOResponse>();
            foreach(var pickup in pickups)
            {
                var pickupResponse = new PickupDTOResponse(
                pickup.Id,
                pickup.ProductId,
                pickup.StartTime,
                pickup.EndTime,
                pickup.Status
                );
                pickupsResponse.Add(pickupResponse);
            }
            return pickupsResponse;
        }

        public async Task<PickupDTOResponse> PostPickup(PickupDTORequest pickupToPost)
        {
            var product = await _productRepository.GetProductAsync((Guid)pickupToPost.ProductId!);

            if(product == null)
            {
                throw new RecourseNotFoundException("product with this id does not exist");
            }

            var restaurant = await _restaurantRepository.GetRestaurantAsync(product.RestaurantID);

            if (restaurant!.Id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException("you are unauthorized to create this resource");

            var pickup = new Pickup(
                Guid.NewGuid(),
                (Guid)pickupToPost.ProductId!,
                (DateTime)pickupToPost.StartTime!,
                (DateTime)pickupToPost.EndTime!,
                (PickupStatus)pickupToPost.Status!
                );

            await _pickupRepository.AddPickupAsync(pickup);
            await _pickupRepository.SaveChangesAsync();

            var pickupResponse = new PickupDTOResponse(
                pickup.Id,
                pickup.ProductId,
                pickup.StartTime,
                pickup.EndTime,
                pickup.Status
                );

            return pickupResponse;
        }

        public async Task<PickupDTOResponse> PutPickup(Guid id, PickupDTORequest pickupToUpdate)
        {
            if (!await _pickupRepository.PickupExistsAsync(id))
            {
                throw new RecourseNotFoundException("pickup with this id does not exist");
            }

            var product = await _productRepository.GetProductAsync((Guid)pickupToUpdate.ProductId!);

            if (product == null)
            {
                throw new RecourseNotFoundException("product with this id does not exist");
            }

            var restaurant = await _restaurantRepository.GetRestaurantAsync(product.RestaurantID);

            if (restaurant!.Id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException("you are unauthorized to create this resource");

            var pickup = new Pickup(
                Guid.NewGuid(),
                (Guid)pickupToUpdate.ProductId!,
                (DateTime)pickupToUpdate.StartTime!,
                (DateTime)pickupToUpdate.EndTime!,
                (PickupStatus)pickupToUpdate.Status!
                );

            _pickupRepository.UpdatePickup(pickup);
            await _pickupRepository.SaveChangesAsync();

            var pickupResponse = new PickupDTOResponse(
                pickup.Id,
                pickup.ProductId,
                pickup.StartTime,
                pickup.EndTime,
                pickup.Status
                );

            return pickupResponse;
        }

    }
}
