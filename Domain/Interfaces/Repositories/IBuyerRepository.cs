
using Domain.Entities;
using Domain.Interfaces.Specifications;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories
{
    public interface IBuyerRepository
    {
        public Task<Buyer?> GetBuyerAsync(Guid id);
        public Buyer? GetBuyer(Guid id);

        public Task<Buyer?> GetBuyerAsync(Expression<Func<Buyer, bool>> predicate);
        public Buyer? GetBuyer(Expression<Func<Buyer, bool>> predicate);

        public Task<IEnumerable<Buyer>> GetBuyersAsync();
        public IEnumerable<Buyer> GetBuyers();


        public Task<IEnumerable<Buyer>> GetBuyersAsync(ISpecification<Buyer> spec);
        public IEnumerable<Buyer> GetBuyers(ISpecification<Buyer> spec);


        public Buyer RemoveBuyer(Buyer Buyer);

        public Buyer UpdateBuyer(Buyer Buyer);


        public Task<Buyer> AddBuyerAsync(Buyer Buyer);
        public Buyer AddBuyer(Buyer Buyer);


        public Task<bool> BuyerExistsAsync(Guid id);
        public bool BuyerExists(Guid id);


        public void SaveChanges();
        public Task SaveChangesAsync();


    }
}
