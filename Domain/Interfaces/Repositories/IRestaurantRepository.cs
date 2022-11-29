using Domain.Entities;
using Domain.Interfaces.Specifications;

namespace Domain.Interfaces.Repositories
{
    public interface IRestaurantRepository
    {
        public Restaurant? GetRestaurant(Guid id);
        public Task<Restaurant?> GetRestaurantAsync(Guid id);


        public Task<Restaurant?> GetRestaurantAsync(Predicate<Restaurant> predicate);
        public Restaurant? GetRestaurant(Predicate<Restaurant> predicate);

        public IEnumerable<Restaurant> GetRestaurants();
        public Task<IEnumerable<Restaurant>> GetRestaurantsAsync();

        public IEnumerable<Restaurant> GetRestaurants(ISpecification<Restaurant> spec);
        public Task<IEnumerable<Restaurant>> GetRestaurantsAsync(ISpecification<Restaurant> spec);

        public Restaurant RemoveRestaurant(Restaurant Restaurant);

        public Restaurant UpdateRestaurant(Restaurant Restaurant);

        public Restaurant AddRestaurant(Restaurant Restaurant);
        public Task<Restaurant> AddRestaurantAsync(Restaurant Restaurant);

        public Task<bool> RestaurantExistsAsync(Guid id);
        public bool RestaurantExists(Guid id);

        public void SaveChanges();

        public Task SaveChangesAsync();

    }
}
