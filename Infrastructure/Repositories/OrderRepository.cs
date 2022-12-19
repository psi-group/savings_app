
using Domain.Entities.OrderAggregate;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SavingsAppContext _appContext;

        public OrderRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Order> AddOrderAsync(Order Order)
        {
            await _appContext.Orders.AddAsync(Order);

            return Order;
        }

        public async Task<Order?> GetOrderAsync(Guid id)
        {
            return await _appContext.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _appContext.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(ISpecification<Order> spec)
        {
            var res = spec.Includes
                .Aggregate(_appContext.Orders.AsQueryable(),
                    (current, include) => current.Include(include));

            res = spec.IncludeStrings
                .Aggregate(res,
                    (current, include) => current.Include(include));



            if (spec.IsPagingEnabled)
            {
                res = res.Skip(spec.Skip)
                             .Take(spec.Take);
            }

            if (spec.Criteria != null)
            {
                res = res.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                res = res.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                res = res.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.GroupBy != null)
            {
                res = res.GroupBy(spec.GroupBy).SelectMany(x => x);
            }

            return await res.ToListAsync();
        }

        public async Task<bool> OrderExistsAsync(Guid id)
        {
            return await _appContext.Orders.AnyAsync(e => e.Id == id);
        }


        public Order RemoveOrder(Order Order)
        {
            _appContext.Orders.Remove(Order);
            return Order;
        }

        public Order UpdateOrder(Order Order)
        {
            _appContext.Entry(Order).State = EntityState.Modified;

            return Order;
        }

        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync();
        }

    }
}
