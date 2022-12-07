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

namespace Application.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {

        private readonly IFileSaver _fileSaver;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IHttpContextAccessor httpContext,
            IWebHostEnvironment webHostEnvironment, IConfiguration config,
            IRestaurantRepository restaurantRepository, IFileSaver fileSaver)
        {
            _restaurantRepository = restaurantRepository;
            _httpContext = httpContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
            _fileSaver = fileSaver;
        }

        public async Task<RestaurantDTOResponse> DeleteRestaurant(Guid id)
        {
            if (id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext!.User.Identity!).
                FindFirst("Id")!.Value))
                throw new InvalidIdentityException();
            
            var restaurant = await _restaurantRepository.GetRestaurantAsync(id);
            if (restaurant == null)
            {
                throw new RecourseNotFoundException();
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
                restaurant.ImageName,
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
                restaurant.ImageName,
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
                throw new RecourseNotFoundException();
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
                restaurant.ImageName,
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
                restaurant.ImageName,
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
                id.ToString(),
                (bool)restaurantToPost.Open!,
                restaurantToPost.Description,
                restaurantToPost.ShortDescription,
                restaurantToPost.SiteRef);


            Task saveImageTask = _fileSaver.SaveImage(restaurantToPost.Image, restaurant.ImageName,
                    _webHostEnvironment.ContentRootPath + _config["ImageStorage:ImageFoldersPaths:UserImages"],
                    _config["ImageStorage:ImageExtention"]);
            
            

            await saveImageTask;
            await _restaurantRepository.AddRestaurantAsync(restaurant);
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
                restaurant.ImageName,
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
                throw new InvalidIdentityException();


            if (!await _restaurantRepository.RestaurantExistsAsync(id))
            {
                throw new RecourseAlreadyExistsException();
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
                restaurant.ImageName,
                restaurant.Open,
                restaurant.Description,
                restaurant.ShortDescription,
                restaurant.SiteRef
                );

            return restaurantDTO;
        }
    }
}
