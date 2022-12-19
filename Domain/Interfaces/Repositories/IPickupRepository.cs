using Domain.Entities;
using Domain.Interfaces.Specifications;

namespace Domain.Interfaces.Repositories
{
    public interface IPickupRepository
    {
        public Task<Pickup?> GetPickupAsync(Guid id);
        public Task<IEnumerable<Pickup>> GetPickupsAsync(ISpecification<Pickup> spec);

        public Task<Pickup> AddPickupAsync(Pickup Pickup);

        public Task<bool> PickupExistsAsync(Guid id);

        public Task SaveChangesAsync();
    }
}
