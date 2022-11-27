using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Repositories.Interfaces;
using savings_app_backend.SavingToFile;
using savings_app_backend.Services.Interfaces;
using System.Security.Claims;

namespace savings_app_backend.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {

        private readonly IFileSaver _fileSaver;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IHttpContextAccessor httpContext,
            IWebHostEnvironment webHostEnvironment, IConfiguration config, IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
            _httpContext = httpContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        public async Task<Restaurant> DeleteRestaurant(Guid id)
        {
            if (id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();
            
            var restaurant = await _restaurantRepository.GetRestaurant(id);
            if (restaurant == null)
            {
                throw new RecourseNotFoundException();
            }

            await _restaurantRepository.RemoveRestaurant(restaurant);
            return restaurant;
        }

        public async Task<IEnumerable<Restaurant>> GetFilteredRestaurants(string? search)
        {
            return await _restaurantRepository.GetFilteredRestaurants(
                (restaurant) => String.IsNullOrEmpty(search) || restaurant.Name.ToLower().Contains(search.ToLower()));
        }

        public async Task<Restaurant> GetRestaurant(Guid id)
        {
            var restaurant = await _restaurantRepository.GetRestaurant(id);

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
            return await _restaurantRepository.GetRestaurants();
        }

        public async Task<Restaurant> PostRestaurant(Restaurant restaurant)
        {
            restaurant.Id = Guid.NewGuid();

            var imageName = restaurant.Id.ToString();

            var file = restaurant.ImageFile;

            

            Task saveImageTask = _fileSaver.SaveImage(restaurant.ImageFile, imageName,
                    _config["ImageStorage:ImageFoldersPaths:UserImages"],
                    _config["ImageStorage:ImageFoldersPaths:ImageExtention"]);
            
            restaurant.ImageName = imageName;

            await saveImageTask;
            await _restaurantRepository.AddRestaurant(restaurant);

            

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


            if (!await RestaurantExistsAsync(id))
            {
                throw new RecourseAlreadyExistsException();
            }

            await _restaurantRepository.UpdateRestaurant(restaurant);

            return restaurant;
        }


        private async Task<bool> RestaurantExistsAsync(Guid id)
        {
            return await _restaurantRepository.RestaurantExists(id);
        }
    }
}
