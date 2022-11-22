using savings_app_backend.Models.Entities;

namespace savings_app_backend.Repositories.Interfaces
{
    public interface IBuyerRepository
    {
        public Task<Buyer?> GetBuyer(Guid id);
        public Task<Buyer> RemoveBuyer(Buyer Buyer);

        public Task<IEnumerable<Buyer>> GetBuyers();

        public Task<Buyer> UpdateBuyer(Buyer Buyer);
        public Task<Buyer> AddBuyer(Buyer Buyer);

        public Task<bool> BuyerExists(Guid id);

    }
}
