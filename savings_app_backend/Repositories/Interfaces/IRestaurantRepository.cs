using savings_app_backend.Models.Entities;

namespace savings_app_backend.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        public Task<Restaurant?> GetRestaurant(Guid id);

        public Task<Restaurant> RemoveRestaurant(Restaurant Restaurant);

        public Task<IEnumerable<Restaurant>> GetRestaurants();

        public Task<IEnumerable<Restaurant>> GetFilteredRestaurants(params Func<Restaurant, bool>[] filters);

        public Task<Restaurant> UpdateRestaurant(Restaurant Restaurant);

        public Task<Restaurant> AddRestaurant(Restaurant Restaurant);

        public Task<bool> RestaurantExists(Guid id);

    }
}
