using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities.OrderAggregate;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTOResponse>> GetOrders();

        Task<IEnumerable<OrderDTOResponse>> GetBuyersOrders(Guid buyerId);

        Task<IEnumerable<OrderItemDTOResponse>> GetSellersOrderItems(Guid sellerId);

        Task<OrderDTOResponse> GetOrder(Guid id);

        Task<OrderDTOResponse> PutOrder(Guid id, OrderDTORequest Order);

        Task<OrderDTOResponse> DeleteOrder(Guid id);

        Task<OrderDTOResponse> PostOrder(OrderDTORequest Order);

    }
}
