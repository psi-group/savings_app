using Application.Services.Interfaces;
using Domain.Entities.OrderAggregate;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;

namespace Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> DeleteOrder(Guid id)
        {
            var order = await _orderRepository.GetOrderAsync(id);
            if (order == null)
            {
                throw new RecourseNotFoundException();
            }

            //var product = await _context.Products.FindAsync(order.ProductId);
            //var restaurant = await _context.Restaurants.FindAsync(product.RestaurantID);

            /*if (restaurant.Id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();*/

            _orderRepository.RemoveOrder(order);
            await _orderRepository.SaveChangesAsync();
            return order;
        }

        public Task<IEnumerable<Order>> GetBuyerOrders(Guid buyerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrder(Guid id)
        {
            var order = await _orderRepository.GetOrderAsync(id);

            if (order == null)
            {
                throw new RecourseNotFoundException();
            }
            else
            {
                return order;
            }
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _orderRepository.GetOrdersAsync();
        }

        public async Task<Order> PostOrder(Order order)
        {
            order.GenerateId();
            //order.Id = Guid.NewGuid();

            await _orderRepository.AddOrderAsync(order);
            await _orderRepository.SaveChangesAsync();
            return order;
        }

        public async Task<Order> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                throw new InvalidRequestArgumentsException();
            }

            

            if (!await _orderRepository.OrderExistsAsync(id))
            {
                throw new RecourseNotFoundException();
            }

            _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChangesAsync();
            return order;
        }
    }
}
