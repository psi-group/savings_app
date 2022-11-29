

using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IPickupService
    {
        Task<IEnumerable<Pickup>> GetPickups();

        Task<IEnumerable<Pickup>> GetBuyerPickups(Guid buyerId);

        Task<IEnumerable<Pickup>> GetProductPickups(Guid productId);

        Task<Pickup> BookPickup(Guid pickupId);

        Task<Pickup> GetPickup(Guid id);

        Task<Pickup> PutPickup(Guid id, Pickup Pickup);

        Task<Pickup> DeletePickup(Guid id);

        Task<Pickup> PostPickup(Pickup Pickup);

    }
}
