using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders();

        Task<Order> GetOrder(Guid id);

        Task<Order> PutOrder(Guid id, Order Order);

        Task<Order> DeleteOrder(Guid id);

        Task<Order> PostOrder(Order Order);

    }
}
