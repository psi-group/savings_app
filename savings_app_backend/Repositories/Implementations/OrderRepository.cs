using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Repositories.Interfaces;

namespace savings_app_backend.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SavingsAppContext _appContext;

        public OrderRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Order> AddOrder(Order order)
        {
            _appContext.Orders.Add(order);

            await _appContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> GetOrder(Guid id)
        {
            return await _appContext.Orders.FindAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _appContext.Orders.ToListAsync();
        }

        public async Task<bool> OrderExists(Guid id)
        {
            return await _appContext.Orders.AnyAsync(e => e.Id == id);
        }

        public async Task<Order> RemoveOrder(Order order)
        {

            _appContext.Orders.Remove(order);
            await _appContext.SaveChangesAsync();
            return order;
            
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            _appContext.Entry(order).State = EntityState.Modified;

            await _appContext.SaveChangesAsync();

            return order;
        }
    }
}
