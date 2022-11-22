using savings_app_backend.Models.Entities;

namespace savings_app_backend.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<Order?> GetOrder(Guid id);
        public Task<Order> RemoveOrder(Order order);

        public Task<IEnumerable<Order>> GetOrders();

        public Task<Order> UpdateOrder(Order order);
        public Task<Order> AddOrder(Order order);

        public Task<bool> OrderExists(Guid id);

    }
}
