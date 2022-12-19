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

        public async Task<Pickup> AddPickupAsync(Pickup Pickup)
        {
            await _appContext.Pickups.AddAsync(Pickup);

            return Pickup;
        }


        public async Task<Pickup?> GetPickupAsync(Guid id)
        {
            return await _appContext.Pickups.FindAsync(id);
        }

        public async Task<IEnumerable<Pickup>> GetPickupsAsync(ISpecification<Pickup> spec)
        {
            var res = spec.Includes
                .Aggregate(_appContext.Pickups.AsQueryable(),
                    (current, include) => current.Include(include));

            res = spec.IncludeStrings
                .Aggregate(res,
                    (current, include) => current.Include(include));


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


        public async Task<bool> PickupExistsAsync(Guid id)
        {
            return await _appContext.Pickups.AnyAsync(e => e.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync();
        }
    }
}
