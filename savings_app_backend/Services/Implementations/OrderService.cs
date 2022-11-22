using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.Repositories.Interfaces;
using savings_app_backend.Services.Interfaces;
using System.Security.Claims;

namespace savings_app_backend.Services.Implementations
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
            var order = await _orderRepository.GetOrder(id);
            if (order == null)
            {
                throw new RecourseNotFoundException();
            }

            //var product = await _context.Products.FindAsync(order.ProductId);
            //var restaurant = await _context.Restaurants.FindAsync(product.RestaurantID);

            /*if (restaurant.Id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();*/

            await _orderRepository.RemoveOrder(order);
            return order;
        }

        public Task<IEnumerable<Order>> GetBuyerOrders(Guid buyerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrder(Guid id)
        {
            var order = await _orderRepository.GetOrder(id);

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
            return await _orderRepository.GetOrders();
        }

        public async Task<Order> PostOrder(Order order)
        {
            order.Id = Guid.NewGuid();

            await _orderRepository.AddOrder(order);

            return order;
        }

        public async Task<Order> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                throw new InvalidRequestArgumentsException();
            }

            

            if (!await OrderExistsAsync(id))
            {
                throw new RecourseNotFoundException();
            }

            await _orderRepository.UpdateOrder(order);
            return order;
        }


        private async Task<bool> OrderExistsAsync(Guid id)
        {
            return await _orderRepository.OrderExists(id);
        }
    }
}
