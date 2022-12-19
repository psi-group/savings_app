using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IBuyerService
    {
        Task<IEnumerable<BuyerDTOResponse>> GetBuyers();

        Task<BuyerDTOResponse> GetBuyer(Guid id);

        [PrivateIdentity]
        Task<BuyerPrivateDTOResponse> GetBuyerPrivate(Guid id);

        Task<BuyerDTOResponse> PutBuyer(Guid id, BuyerDTORequest buyer);

        Task<BuyerDTOResponse> DeleteBuyer(Guid id);

        Task<BuyerDTOResponse> PostBuyer(BuyerDTORequest buyer);
    }
}
