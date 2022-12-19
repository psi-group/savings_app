

using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IPickupService
    {
        Task<IEnumerable<PickupDTOResponse>> GetBuyerPickups(Guid buyerId);

        Task<IEnumerable<PickupDTOResponse>> GetProductPickups(Guid productId);

        Task<PickupDTOResponse> GetPickup(Guid id);

        Task<PickupDTOResponse> PostPickup(PickupDTORequest Pickup);

    }
}
