using Application.Services.Interfaces;
using Application.Specifications;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Transactions;

namespace Application.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {

        private readonly IFileSaver _fileSaver;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IHttpContextAccessor httpContext,
            IRestaurantRepository restaurantRepository, IFileSaver fileSaver)
        {
            _restaurantRepository = restaurantRepository;
            _httpContext = httpContext;
            _fileSaver = fileSaver;
        }

        public async Task<RestaurantDTOResponse> DeleteRestaurant(Guid id)
        {
            if (id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext!.User.Identity!).
                FindFirst("Id")!.Value))
                throw new InvalidIdentityException("you are unauthorized to delete this resource");
            
            var restaurant = await _restaurantRepository.GetRestaurantAsync(id);
            if (restaurant == null)
            {
                throw new RecourseNotFoundException("restaurant with this id does not exist");
            }

            _restaurantRepository.RemoveRestaurant(restaurant);
            await _restaurantRepository.SaveChangesAsync();

            var restaurantDTO = new RestaurantDTOResponse(
                restaurant.Id,
                restaurant.Name,
                new AddressDTOResponse(
                    restaurant.Address.Country,
                    restaurant.Address.City,
                    restaurant.Address.StreetName,
                    restaurant.Address.HouseNumber,
                    restaurant.Address.AppartmentNumber,
                    restaurant.Address.PostalCode
                    ),
                restaurant.ImageUrl,
                restaurant.Open,
                restaurant.Description,
                restaurant.ShortDescription,
                restaurant.SiteRef
                );

            return restaurantDTO;
        }

        public async Task<IEnumerable<RestaurantDTOResponse>> GetFilteredRestaurants(string? search)
        {
            var spec = new RestaurantFilterSpecification(search);
            var restaurants = _restaurantRepository.GetRestaurants(spec);
            var restaurantsDTO = new List<RestaurantDTOResponse>();
            foreach(var restaurant in restaurants)
            {
                var restaurantDTO = new RestaurantDTOResponse(
                restaurant.Id,
                restaurant.Name,
                new AddressDTOResponse(
                    restaurant.Address.Country,
                    restaurant.Address.City,
                    restaurant.Address.StreetName,
                    restaurant.Address.HouseNumber,
                    restaurant.Address.AppartmentNumber,
                    restaurant.Address.PostalCode
                    ),
                restaurant.ImageUrl,
                restaurant.Open,
                restaurant.Description,
                restaurant.ShortDescription,
                restaurant.SiteRef
                );

                restaurantsDTO.Add(restaurantDTO);
            }

            return restaurantsDTO;
        }

        public async Task<RestaurantDTOResponse> GetRestaurant(Guid id)
        {
            var restaurant = await _restaurantRepository.GetRestaurantAsync(id);

            if (restaurant == null)
            {
                throw new RecourseNotFoundException("restaurant with this id does not exist");
            }
            else
            {
                var restaurantDTO = new RestaurantDTOResponse(
                restaurant.Id,
                restaurant.Name,
                new AddressDTOResponse(
                    restaurant.Address.Country,
                    restaurant.Address.City,
                    restaurant.Address.StreetName,
                    restaurant.Address.HouseNumber,
                    restaurant.Address.AppartmentNumber,
                    restaurant.Address.PostalCode
                    ),
                restaurant.ImageUrl,
                restaurant.Open,
                restaurant.Description,
                restaurant.ShortDescription,
                restaurant.SiteRef
                );

                return restaurantDTO;
            }
        }

        public async Task<RestaurantPrivateDTOResponse> GetRestaurantPrivate(Guid id)
        {
            if (id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException("you are unauthorized to access this resource");

            var restaurant = await _restaurantRepository.GetRestaurantAsync(id);

            if (restaurant == null)
            {
                throw new RecourseNotFoundException("restaurant with this id does not exist");
            }
            else
            {
                var restaurantDTO = new RestaurantPrivateDTOResponse(
                restaurant.Id,
                restaurant.Name,
                new AddressDTOResponse(
                    restaurant.Address.Country,
                    restaurant.Address.City,
                    restaurant.Address.StreetName,
                    restaurant.Address.HouseNumber,
                    restaurant.Address.AppartmentNumber,
                    restaurant.Address.PostalCode
                    ),
                new UserAuthDTOResponse(
                    restaurant.UserAuth.Email
                    ),
                restaurant.ImageUrl,
                restaurant.Open,
                restaurant.Description,
                restaurant.ShortDescription,
                restaurant.SiteRef
                );

                return restaurantDTO;
            }
        }

        public async Task<IEnumerable<RestaurantDTOResponse>> GetRestaurants()
        {
            var restaurants = await _restaurantRepository.GetRestaurantsAsync();
            var restaurantsDTO = new List<RestaurantDTOResponse>();
            foreach (var restaurant in restaurants)
            {
                var restaurantDTO = new RestaurantDTOResponse(
                restaurant.Id,
                restaurant.Name,
                new AddressDTOResponse(
                    restaurant.Address.Country,
                    restaurant.Address.City,
                    restaurant.Address.StreetName,
                    restaurant.Address.HouseNumber,
                    restaurant.Address.AppartmentNumber,
                    restaurant.Address.PostalCode
                    ),
                restaurant.ImageUrl,
                restaurant.Open,
                restaurant.Description,
                restaurant.ShortDescription,
                restaurant.SiteRef
                );

                restaurantsDTO.Add(restaurantDTO);
            }

            return restaurantsDTO;

        }

        public async Task<RestaurantDTOResponse> PostRestaurant(RestaurantDTORequest restaurantToPost)
        {
            if(await _restaurantRepository.GetRestaurantAsync(
                restaurant => restaurant.UserAuth.Email == restaurantToPost.UserAuth!.Email) != null)
            {
                throw new RecourseAlreadyExistsException("restaurant with this email already exists");
            }
            var id = Guid.NewGuid();
            var restaurant = new Restaurant(
                id,
                restaurantToPost.Name!,
                new UserAuth(
                    restaurantToPost.UserAuth!.Password!,
                    restaurantToPost.UserAuth!.Email!),
                new Address(
                    restaurantToPost.Address!.Country!,
                    restaurantToPost.Address.City!,
                    restaurantToPost.Address.StreetName!,
                    (int)restaurantToPost.Address.HouseNumber!,
                    restaurantToPost.Address.AppartmentNumber,
                    (int)restaurantToPost.Address.PostalCode!),
                restaurantToPost.Image == null ? null :
                "https://savingsapp.blob.core.windows.net/userimages/" + id + ".jpg",
                (bool)restaurantToPost.Open!,
                restaurantToPost.Description,
                restaurantToPost.ShortDescription,
                restaurantToPost.SiteRef);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                
                await _restaurantRepository.AddRestaurantAsync(restaurant);
                await _restaurantRepository.SaveChangesAsync();

                if (restaurantToPost.Image != null)
                {
                    Task saveImageTask = _fileSaver.SaveImage
                    (restaurantToPost.Image, restaurant.Id.ToString(), false);
                    await saveImageTask;

                    
                }
                scope.Complete();
            }
            
            var restaurantDTO = new RestaurantDTOResponse(
                restaurant.Id,
                restaurant.Name,
                new AddressDTOResponse(
                    restaurant.Address.Country,
                    restaurant.Address.City,
                    restaurant.Address.StreetName,
                    restaurant.Address.HouseNumber,
                    restaurant.Address.AppartmentNumber,
                    restaurant.Address.PostalCode
                    ),
                restaurant.ImageUrl,
                restaurant.Open,
                restaurant.Description,
                restaurant.ShortDescription,
                restaurant.SiteRef
                );

            return restaurantDTO;
        }

        public async Task<RestaurantDTOResponse> PutRestaurant(Guid id, RestaurantDTORequest restaurantToUpdate)
        {
            if (id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException("you are unauthorized to update this resource");

            if (await _restaurantRepository.GetRestaurantAsync(
                restaurant => restaurant.UserAuth.Email == restaurantToUpdate.UserAuth!.Email) != null)
            {
                throw new RecourseAlreadyExistsException("restaurant with this email already exists");
            }

            if (!await _restaurantRepository.RestaurantExistsAsync(id))
            {
                throw new RecourseNotFoundException("restaurant with this id does not exist");
            }

            var restaurant = new Restaurant(
                id,
                restaurantToUpdate.Name!,
                new UserAuth(
                    restaurantToUpdate.UserAuth!.Password!,
                    restaurantToUpdate.UserAuth!.Email!),
                new Address(
                    restaurantToUpdate.Address!.Country!,
                    restaurantToUpdate.Address.City!,
                    restaurantToUpdate.Address.StreetName!,
                    (int)restaurantToUpdate.Address.HouseNumber!,
                    restaurantToUpdate.Address.AppartmentNumber,
                    (int)restaurantToUpdate.Address.PostalCode!),
                id.ToString(),
                (bool)restaurantToUpdate.Open!,
                restaurantToUpdate.Description,
                restaurantToUpdate.ShortDescription,
                restaurantToUpdate.SiteRef);

            _restaurantRepository.UpdateRestaurant(restaurant);
            await _restaurantRepository.SaveChangesAsync();


            var restaurantDTO = new RestaurantDTOResponse(
                restaurant.Id,
                restaurant.Name,
                new AddressDTOResponse(
                    restaurant.Address.Country,
                    restaurant.Address.City,
                    restaurant.Address.StreetName,
                    restaurant.Address.HouseNumber,
                    restaurant.Address.AppartmentNumber,
                    restaurant.Address.PostalCode
                    ),
                restaurant.ImageUrl,
                restaurant.Open,
                restaurant.Description,
                restaurant.ShortDescription,
                restaurant.SiteRef
                );

            return restaurantDTO;
        }
    }
}
