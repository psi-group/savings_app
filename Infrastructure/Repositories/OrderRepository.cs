
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

        public Order AddOrder(Order Order)
        {
            _appContext.Orders.Add(Order);

            return Order;
        }
        public async Task<Order> AddOrderAsync(Order Order)
        {
            await _appContext.Orders.AddAsync(Order);

            return Order;
        }


        public Order? GetOrder(Guid id)
        {
            return _appContext.Orders.Find(id);
        }
        public async Task<Order?> GetOrderAsync(Guid id)
        {
            return await _appContext.Orders.FindAsync(id);
        }



        public IEnumerable<Order> GetOrders()
        {
            return _appContext.Orders.ToList();
        }
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _appContext.Orders.ToListAsync();
        }


        public IEnumerable<Order> GetOrders(ISpecification<Order> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_appContext.Orders.AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                            .Where(spec.Criteria)
                            .ToList();
        }
        public async Task<IEnumerable<Order>> GetOrdersAsync(ISpecification<Order> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_appContext.Orders.AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return await secondaryResult
                            .Where(spec.Criteria)
                            .ToListAsync();
        }

        public bool OrderExists(Guid id)
        {
            return _appContext.Orders.Any(e => e.Id == id);
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

        public void SaveChanges()
        {
            _appContext.SaveChanges();
        }
    }
}
