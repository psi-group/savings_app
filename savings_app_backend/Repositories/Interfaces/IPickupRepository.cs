using savings_app_backend.Models.Entities;

namespace savings_app_backend.Repositories.Interfaces
{
    public interface IPickupRepository
    {
        public Task<Pickup?> GetPickup(Guid id);
        public Task<Pickup> RemovePickup(Pickup pickup);

        public Task<IEnumerable<Pickup>> GetPickups();

        public Task<Pickup> UpdatePickup(Pickup pickup);
        public Task<Pickup> AddPickup(Pickup pickup);

        public Task<bool> PickupExists(Guid id);

    }
}
