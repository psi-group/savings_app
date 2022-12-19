using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly SavingsAppContext _appContext;

        public BuyerRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Buyer> AddBuyerAsync(Buyer Buyer)
        {
            await _appContext.Buyers.AddAsync(Buyer);

            return Buyer;
        }

        public async Task<Buyer?> GetBuyerAsync(Guid id)
        {
            return await _appContext.Buyers.FindAsync(id);
        }

        public async Task<IEnumerable<Buyer>> GetBuyersAsync()
        {
            return await _appContext.Buyers.ToListAsync();
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


        public async Task<Buyer?> GetBuyerAsync(Expression<Func<Buyer, bool>> predicate)
        {
            return await _appContext.Buyers.FirstOrDefaultAsync(predicate);
        }
    }
}
