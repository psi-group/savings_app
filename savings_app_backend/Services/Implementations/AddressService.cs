/*using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.Services.Interfaces;
using System.Security.Claims;

namespace savings_app_backend.Services.Implementations
{
    public class AddressService : IAddressService
    {
        private readonly SavingsAppContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public AddressService(SavingsAppContext savingsAppContext,
            IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _context = savingsAppContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        /*public async Task<Address> DeleteAddress(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                throw new RecourseNotFoundException();
            }

            //var product = await _context.Products.FindAsync(Address.ProductId);
            //var restaurant = await _context.Restaurants.FindAsync(product.RestaurantID);

            /*if (restaurant.Id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();*/
/*
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public Task<IEnumerable<Address>> GetBuyerAddresss(Guid buyerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Address> GetAddress(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if(address == null)
            {
                throw new RecourseNotFoundException();
            }
            else
            {
                return address;
            }
        }

        public async Task<IEnumerable<Address>> GetAddresses()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> PostAddress(Address address)
        {
            address.Id = Guid.NewGuid();

            _context.Addresses.Add(address);

            await _context.SaveChangesAsync();

            return address;
        }

        public async Task<Address> PutAddress(Guid id, Address address)
        {
            if (id != address.Id)
            {
                throw new InvalidRequestArgumentsException();
            }

            _context.Entry(address).State = EntityState.Modified;

            if (!await AddressExistsAsync(id))
            {
                throw new RecourseNotFoundException();
            }

            await _context.SaveChangesAsync();
            return address;
        }


        private async Task<bool> AddressExistsAsync(Guid id)
        {
            return await _context.Addresses.AnyAsync(e => e.Id == id);
        }
    }
}
*/