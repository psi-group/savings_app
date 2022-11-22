using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurant>> GetRestaurants();
        Task<IEnumerable<Restaurant>> GetFilteredRestaurants(string? search);

        Task<Restaurant> GetRestaurant(Guid id);

        Task<Restaurant> PutRestaurant(Guid id, Restaurant restaurant);

        Task<Restaurant> DeleteRestaurant(Guid id);

        Task<Restaurant> PostRestaurant(Restaurant restaurant);

    }
}
