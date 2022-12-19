using Domain.Entities;
using Domain.Interfaces.Specifications;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories
{
    public interface IRestaurantRepository
    {
        public Task<Restaurant?> GetRestaurantAsync(Guid id);

        public Task<Restaurant?> GetRestaurantAsync(Expression<Func<Restaurant, bool>> predicate);

        public Task<IEnumerable<Restaurant>> GetRestaurantsAsync();

        public Task<IEnumerable<Restaurant>> GetRestaurantsAsync(ISpecification<Restaurant> spec);

        public Restaurant RemoveRestaurant(Restaurant Restaurant);

        public Restaurant UpdateRestaurant(Restaurant Restaurant);

        public Task<Restaurant> AddRestaurantAsync(Restaurant Restaurant);

        public Task<bool> RestaurantExistsAsync(Guid id);

        public Task SaveChangesAsync();

    }
}
