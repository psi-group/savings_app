using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.Services.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAddresses();

        Task<Address> GetAddress(Guid id);

        Task<Address> PutAddress(Guid id, Address Address);

        Task<Address> DeleteAddress(Guid id);

        Task<Address> PostAddress(Address Address);

    }
}
