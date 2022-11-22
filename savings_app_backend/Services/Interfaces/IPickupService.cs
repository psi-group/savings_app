using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.Services.Interfaces
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
