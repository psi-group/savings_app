using Domain.Entities.OrderAggregate;
using Domain.Interfaces.Specifications;

namespace Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        public Task<Order?> GetOrderAsync(Guid id);
        public Order? GetOrder(Guid id);


        public Task<IEnumerable<Order>> GetOrdersAsync();
        public IEnumerable<Order> GetOrders();


        public Task<IEnumerable<Order>> GetOrdersAsync(ISpecification<Order> spec);
        public IEnumerable<Order> GetOrders(ISpecification<Order> spec);

        public Order RemoveOrder(Order Order);

        public Order UpdateOrder(Order Order);

        public Task<Order> AddOrderAsync(Order Order);
        public Order AddOrder(Order Order);

        public Task<bool> OrderExistsAsync(Guid id);
        public bool OrderExists(Guid id);

        public void SaveChanges();

        public Task SaveChangesAsync();
    }
}
