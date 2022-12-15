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
            var res = spec.Includes
                .Aggregate(_appContext.Pickups.AsQueryable(),
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
        public async Task<IEnumerable<Pickup>> GetPickupsAsync(ISpecification<Pickup> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var res = spec.Includes
                .Aggregate(_appContext.Pickups.AsQueryable(),
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
