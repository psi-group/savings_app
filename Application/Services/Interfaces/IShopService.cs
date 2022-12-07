using Domain.DTOs.Request;
using Domain.Entities.OrderAggregate;

namespace Application.Services.Interfaces
{
    public interface IShopService
    {
        public Task<Order> Checkout(CheckoutDTORequest checkout);
    }
}
