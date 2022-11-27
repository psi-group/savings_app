using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Repositories.Interfaces;

namespace savings_app_backend.Repositories.Implementations
{
    public class PickupRepository : IPickupRepository
    {
        private readonly SavingsAppContext _appContext;

        public PickupRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Pickup> AddPickup(Pickup pickup)
        {
            _appContext.Pickups.Add(pickup);

            await _appContext.SaveChangesAsync();

            return pickup;
        }

        public async Task<Pickup?> GetPickup(Guid id)
        {
            return await _appContext.Pickups.FindAsync(id);
        }

        public async Task<IEnumerable<Pickup>> GetPickups()
        {
            return await _appContext.Pickups.ToListAsync();
        }

        public async Task<bool> PickupExists(Guid id)
        {
            return await _appContext.Pickups.AnyAsync(e => e.Id == id);
        }

        public async Task<Pickup> RemovePickup(Pickup pickup)
        {

            _appContext.Pickups.Remove(pickup);
            await _appContext.SaveChangesAsync();
            return pickup;
            
        }

        public async Task<Pickup> UpdatePickup(Pickup pickup)
        {
            _appContext.Entry(pickup).State = EntityState.Modified;

            await _appContext.SaveChangesAsync();

            return pickup;
        }
    }
}
