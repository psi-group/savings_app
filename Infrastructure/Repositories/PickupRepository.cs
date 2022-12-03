using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PickupRepository : IPickupRepository
    {
        private readonly SavingsAppContext _appContext;

        public PickupRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public Pickup AddPickup(Pickup Pickup)
        {
            _appContext.Pickups.Add(Pickup);

            return Pickup;
        }
        public async Task<Pickup> AddPickupAsync(Pickup Pickup)
        {
            await _appContext.Pickups.AddAsync(Pickup);

            return Pickup;
        }


        public Pickup? GetPickup(Guid id)
        {
            return _appContext.Pickups.Find(id);
        }
        public async Task<Pickup?> GetPickupAsync(Guid id)
        {
            return await _appContext.Pickups.FindAsync(id);
        }



        public IEnumerable<Pickup> GetPickups()
        {
            return _appContext.Pickups.ToList();
        }
        public async Task<IEnumerable<Pickup>> GetPickupsAsync()
        {
            return await _appContext.Pickups.ToListAsync();
        }


        public IEnumerable<Pickup> GetPickups(ISpecification<Pickup> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_appContext.Pickups.AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                            .Where(spec.Criteria)
                            .ToList();
        }
        public async Task<IEnumerable<Pickup>> GetPickupsAsync(ISpecification<Pickup> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_appContext.Pickups.AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return await secondaryResult
                            .Where(spec.Criteria)
                            .ToListAsync();
        }



        public bool PickupExists(Guid id)
        {
            return _appContext.Pickups.Any(e => e.Id == id);
        }
        public async Task<bool> PickupExistsAsync(Guid id)
        {
            return await _appContext.Pickups.AnyAsync(e => e.Id == id);
        }


        public Pickup RemovePickup(Pickup Pickup)
        {
            _appContext.Pickups.Remove(Pickup);
            return Pickup;
        }

        public Pickup UpdatePickup(Pickup Pickup)
        {
            _appContext.Entry(Pickup).State = EntityState.Modified;

            return Pickup;
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
