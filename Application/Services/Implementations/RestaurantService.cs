using Application.Services.Interfaces;
using Application.Specifications;
using Domain.DTOs.Request;
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

        public async Task<Restaurant> DeleteRestaurant(Guid id)
        {
            if (id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();
            
            var restaurant = await _restaurantRepository.GetRestaurantAsync(id);
            if (restaurant == null)
            {
                throw new RecourseNotFoundException();
            }

            _restaurantRepository.RemoveRestaurant(restaurant);
            await _restaurantRepository.SaveChangesAsync();
            return restaurant;
        }

        public async Task<IEnumerable<Restaurant>> GetFilteredRestaurants(string? search)
        {
            var spec = new RestaurantFilterSpecification(search);
            return _restaurantRepository.GetRestaurants(spec);
        }

        public async Task<Restaurant> GetRestaurant(Guid id)
        {
            var restaurant = await _restaurantRepository.GetRestaurantAsync(id);

            if (restaurant == null)
            {
                throw new RecourseNotFoundException();
            }
            else
            {
                return restaurant;
            }
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurants()
        {
            return await _restaurantRepository.GetRestaurantsAsync();
        }

        public async Task<Restaurant> PostRestaurant(RestaurantDTORequest restaurantToPost)
        {

            var id = Guid.NewGuid();
            var restaurant = new Restaurant(
                id,
                restaurantToPost.Name,
                new UserAuth(
                    restaurantToPost.UserAuth.Password,
                    restaurantToPost.UserAuth.Email),
                new Address(
                    restaurantToPost.Address.Country,
                    restaurantToPost.Address.City,
                    restaurantToPost.Address.StreetName,
                    restaurantToPost.Address.HouseNumber,
                    restaurantToPost.Address.AppartmentNumber,
                    restaurantToPost.Address.PostalCode),
                id.ToString(),
                restaurantToPost.Open,
                restaurantToPost.Description,
                restaurantToPost.ShortDescription,
                restaurantToPost.SiteRef);

            var a = _config["ImageStorage:ImageExtention"];
            var b = _webHostEnvironment.ContentRootPath + _config["ImageStorage:ImageFoldersPaths:UserImages"];

            Task saveImageTask = _fileSaver.SaveImage(restaurantToPost.Image, restaurant.ImageName,
                    _webHostEnvironment.ContentRootPath + _config["ImageStorage:ImageFoldersPaths:UserImages"],
                    _config["ImageStorage:ImageExtention"]);
            
            

            await saveImageTask;
            await _restaurantRepository.AddRestaurantAsync(restaurant);
            await _restaurantRepository.SaveChangesAsync();

            return restaurant;
        }

        public async Task<Restaurant> PutRestaurant(Guid id, Restaurant restaurant)
        {
            if (id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();
            
            if (id != restaurant.Id)
            {
                throw new InvalidRequestArgumentsException();
            }


            if (!await _restaurantRepository.RestaurantExistsAsync(id))
            {
                throw new RecourseAlreadyExistsException();
            }

            _restaurantRepository.UpdateRestaurant(restaurant);
            await _restaurantRepository.SaveChangesAsync();
            return restaurant;
        }
    }
}
