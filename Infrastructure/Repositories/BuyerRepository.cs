using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly SavingsAppContext _appContext;

        public BuyerRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public Buyer AddBuyer(Buyer Buyer)
        {
            _appContext.Buyers.Add(Buyer);

            return Buyer;
        }
        public async Task<Buyer> AddBuyerAsync(Buyer Buyer)
        {
            await _appContext.Buyers.AddAsync(Buyer);

            return Buyer;
        }


        public Buyer? GetBuyer(Guid id)
        {
            return _appContext.Buyers.Find(id);
        }
        public async Task<Buyer?> GetBuyerAsync(Guid id)
        {
            return await _appContext.Buyers.FindAsync(id);
        }



        public IEnumerable<Buyer> GetBuyers()
        {
            return _appContext.Buyers.ToList();
        }
        public async Task<IEnumerable<Buyer>> GetBuyersAsync()
        {
            return await _appContext.Buyers.ToListAsync();
        }


        public IEnumerable<Buyer> GetBuyers(ISpecification<Buyer> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_appContext.Buyers.AsQueryable(),
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
        public async Task<IEnumerable<Buyer>> GetBuyersAsync(ISpecification<Buyer> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_appContext.Buyers.AsQueryable(),
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



        public bool BuyerExists(Guid id)
        {
            return _appContext.Buyers.Any(e => e.Id == id);
        }
        public async Task<bool> BuyerExistsAsync(Guid id)
        {
            return await _appContext.Buyers.AnyAsync(e => e.Id == id);
        }


        public Buyer RemoveBuyer(Buyer Buyer)
        {
            _appContext.Buyers.Remove(Buyer);
            return Buyer;
        }

        public Buyer UpdateBuyer(Buyer Buyer)
        {
            _appContext.Entry(Buyer).State = EntityState.Modified;

            return Buyer;
        }

        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _appContext.SaveChanges();
        }

        public async Task<Buyer?> GetBuyerAsync(Predicate<Buyer> predicate)
        {
            return await _appContext.Buyers.FirstOrDefaultAsync(e => predicate(e));
        }

        public Buyer? GetBuyer(Predicate<Buyer> predicate)
        {
            return _appContext.Buyers.FirstOrDefault(e => predicate(e));
        }
    }
}
