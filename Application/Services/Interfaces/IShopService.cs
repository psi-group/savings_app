using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities.OrderAggregate;

namespace Application.Services.Interfaces
{
    public interface IShopService
    {
        public Task<OrderDTOResponse> Checkout(CheckoutDTORequest checkout);
    }
}
