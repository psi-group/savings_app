using Domain.Entities;
using Domain.Interfaces.Specifications;

namespace Domain.Interfaces.Repositories
{
    public interface IPickupRepository
    {
        public Task<Pickup?> GetPickupAsync(Guid id);
        public Pickup? GetPickup(Guid id);


        public Task<IEnumerable<Pickup>> GetPickupsAsync();
        public IEnumerable<Pickup> GetPickups();


        public Task<IEnumerable<Pickup>> GetPickupsAsync(ISpecification<Pickup> spec);
        public IEnumerable<Pickup> GetPickups(ISpecification<Pickup> spec);

        public Pickup RemovePickup(Pickup Pickup);

        public Pickup UpdatePickup(Pickup Pickup);

        public Task<Pickup> AddPickupAsync(Pickup Pickup);
        public Pickup AddPickup(Pickup Pickup);

        public Task<bool> PickupExistsAsync(Guid id);
        public bool PickupExists(Guid id);

        public void SaveChanges();

        public Task SaveChangesAsync();
    }
}
