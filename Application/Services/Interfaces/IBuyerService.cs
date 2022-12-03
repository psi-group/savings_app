using Domain.DTOs.Request;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IBuyerService
    {
        Task<IEnumerable<Buyer>> GetBuyers();

        Task<Buyer> GetBuyer(Guid id);

        Task<Buyer> PutBuyer(Guid id, Buyer buyer);

        Task<Buyer> DeleteBuyer(Guid id);

        Task<Buyer> PostBuyer(BuyerDTORequest buyer);
    }
}
