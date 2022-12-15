using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly SavingsAppContext _appContext;

        public RestaurantRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Restaurant> AddRestaurantAsync(Restaurant Restaurant)
        {
            await _appContext.Restaurants.AddAsync(Restaurant);

            return Restaurant;
        }
        public Restaurant AddRestaurant(Restaurant Restaurant)
        {
            _appContext.Restaurants.Add(Restaurant);

            return Restaurant;
        }


        public async Task<Restaurant?> GetRestaurantAsync(Expression<Func<Restaurant, bool>> predicate)
        {
            return await _appContext.Restaurants.FirstOrDefaultAsync(predicate);
        }

        public Restaurant? GetRestaurant(Expression<Func<Restaurant, bool>> predicate)
        {
            return _appContext.Restaurants.FirstOrDefault(predicate);
        }


        public async Task<Restaurant?> GetRestaurantAsync(Guid id)
        {
            return await _appContext.Restaurants.FindAsync(id);
        }
        public Restaurant? GetRestaurant(Guid id)
        {
            return _appContext.Restaurants.Find(id);
        }



        public IEnumerable<Restaurant> GetRestaurants()
        {
            return _appContext.Restaurants.ToList();
        }
        public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync()
        {
            return await _appContext.Restaurants.ToListAsync();
        }


        public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync(ISpecification<Restaurant> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var res = spec.Includes
                .Aggregate(_appContext.Restaurants.AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            res = spec.IncludeStrings
                .Aggregate(res,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression


            if (spec.IsPagingEnabled)
            {
                res = res.Skip(spec.Skip)
                             .Take(spec.Take);
            }

            if (spec.Criteria != null)
            {
                res = res.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                res = res.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                res = res.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.GroupBy != null)
            {
                res = res.GroupBy(spec.GroupBy).SelectMany(x => x);
            }

            return await res.ToListAsync();
        }
        public  IEnumerable<Restaurant> GetRestaurants(ISpecification<Restaurant> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var res = spec.Includes
                .Aggregate(_appContext.Restaurants.AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            res = spec.IncludeStrings
                .Aggregate(res,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression


            if (spec.IsPagingEnabled)
            {
                res = res.Skip(spec.Skip)
                             .Take(spec.Take);
            }

            if (spec.Criteria != null)
            {
                res = res.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                res = res.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                res = res.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.GroupBy != null)
            {
                res = res.GroupBy(spec.GroupBy).SelectMany(x => x);
            }

            return res.ToList();
        }



        public bool RestaurantExists(Guid id)
        {
            return _appContext.Restaurants.Any(e => e.Id == id);
        }
        public async Task<bool> RestaurantExistsAsync(Guid id)
        {
            return await _appContext.Restaurants.AnyAsync(e => e.Id == id);
        }



        public Restaurant RemoveRestaurant(Restaurant Restaurant)
        {
            _appContext.Restaurants.Remove(Restaurant);
            return Restaurant;
        }


        public Restaurant UpdateRestaurant(Restaurant Restaurant)
        {
            _appContext.Entry(Restaurant).State = EntityState.Modified;

            return Restaurant;
        }

        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _appContext.SaveChanges();
        }

    }
}
