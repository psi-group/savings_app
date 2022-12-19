using Domain.Entities.OrderAggregate;
using Domain.Interfaces.Specifications;

namespace Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        public Task<Order?> GetOrderAsync(Guid id);

        public Task<IEnumerable<Order>> GetOrdersAsync();

        public Task<IEnumerable<Order>> GetOrdersAsync(ISpecification<Order> spec);

        public Order RemoveOrder(Order Order);

        public Order UpdateOrder(Order Order);

        public Task<Order> AddOrderAsync(Order Order);

        public Task<bool> OrderExistsAsync(Guid id);
        public Task SaveChangesAsync();
    }
}
