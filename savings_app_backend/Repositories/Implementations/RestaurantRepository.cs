using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Repositories.Interfaces;
using System.Linq;

namespace savings_app_backend.Repositories.Implementations
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly SavingsAppContext _appContext;

        public RestaurantRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Restaurant> AddRestaurant(Restaurant Restaurant)
        {
            _appContext.Restaurants.Add(Restaurant);

            await _appContext.SaveChangesAsync();

            return Restaurant;
        }

        public async Task<Restaurant?> GetRestaurant(Guid id)
        {
            return await _appContext.Restaurants.FindAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurants()
        {
            return await _appContext.Restaurants.ToListAsync();
        }

        public async Task<bool> RestaurantExists(Guid id)
        {
            return await _appContext.Restaurants.AnyAsync(e => e.Id == id);
        }

        public async Task<Restaurant> RemoveRestaurant(Restaurant Restaurant)
        {

            _appContext.Restaurants.Remove(Restaurant);
            await _appContext.SaveChangesAsync();
            return Restaurant;
            
        }

        public async Task<Restaurant> UpdateRestaurant(Restaurant Restaurant)
        {
            _appContext.Entry(Restaurant).State = EntityState.Modified;

            await _appContext.SaveChangesAsync();

            return Restaurant;
        }

        public async Task<IEnumerable<Restaurant>> GetFilteredRestaurants(params Func<Restaurant, bool>[] filters)
        {
            IEnumerable<Restaurant> filteredRestaurants = null;

            if (filters.Length > 0)
            {
                filteredRestaurants = _appContext.Restaurants.Where(filters[0]);
            }

            for (int i = 1; i < filters.Length; ++i)
            {
                filteredRestaurants = filteredRestaurants.Where(filters[i]);
            }

            return filteredRestaurants.ToList();
        }
    }
}
