
using Domain.Entities;
using Domain.Interfaces.Specifications;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories
{
    public interface IBuyerRepository
    {
        public Task<Buyer?> GetBuyerAsync(Guid id);

        public Task<Buyer?> GetBuyerAsync(Expression<Func<Buyer, bool>> predicate);

        public Task<IEnumerable<Buyer>> GetBuyersAsync();

        public Buyer RemoveBuyer(Buyer Buyer);

        public Buyer UpdateBuyer(Buyer Buyer);


        public Task<Buyer> AddBuyerAsync(Buyer Buyer);


        public Task<bool> BuyerExistsAsync(Guid id);

        public Task SaveChangesAsync();
    }
}
