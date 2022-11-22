using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.Services.Interfaces
{
    public interface IBuyerService
    {
        Task<IEnumerable<Buyer>> GetBuyers();

        Task<Buyer> GetBuyer(Guid id);

        Task<Buyer> PutBuyer(Guid id, Buyer buyer);

        Task<Buyer> DeleteBuyer(Guid id);

        Task<Buyer> PostBuyer(Buyer buyer);
    }
}
