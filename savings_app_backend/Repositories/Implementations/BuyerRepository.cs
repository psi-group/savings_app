using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Repositories.Interfaces;

namespace savings_app_backend.Repositories.Implementations
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly SavingsAppContext _appContext;

        public BuyerRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Buyer> AddBuyer(Buyer buyer)
        {
            _appContext.Buyers.Add(buyer);

            await _appContext.SaveChangesAsync();

            return buyer;
        }

        public async Task<Buyer?> GetBuyer(Guid id)
        {
            return await _appContext.Buyers.FindAsync();
        }

        public async Task<IEnumerable<Buyer>> GetBuyers()
        {
            return await _appContext.Buyers.ToListAsync();
        }

        public async Task<bool> BuyerExists(Guid id)
        {
            return await _appContext.Buyers.AnyAsync(e => e.Id == id);
        }

        public async Task<Buyer> RemoveBuyer(Buyer buyer)
        {

            _appContext.Buyers.Remove(buyer);
            await _appContext.SaveChangesAsync();
            return buyer;
            
        }

        public async Task<Buyer> UpdateBuyer(Buyer buyer)
        {
            _appContext.Entry(buyer).State = EntityState.Modified;

            await _appContext.SaveChangesAsync();

            return buyer;
        }
    }
}
