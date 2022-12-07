using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
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

        public async Task<OrderDTOResponse> DeleteOrder(Guid id)
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

            var orderDTO = new OrderDTOResponse(
                order.Id,
                order.BuyerId,
                new List<OrderItem>(order.OrderItems)
                );

            return orderDTO;
        }

        public Task<IEnumerable<Order>> GetBuyerOrders(Guid buyerId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDTOResponse> GetOrder(Guid id)
        {
            var order = await _orderRepository.GetOrderAsync(id);

            if (order == null)
            {
                throw new RecourseNotFoundException();
            }
            else
            {
                var orderDTO = new OrderDTOResponse(
                order.Id,
                order.BuyerId,
                new List<OrderItem>(order.OrderItems)
                );

                return orderDTO;
            }
        }

        public async Task<IEnumerable<OrderDTOResponse>> GetOrders()
        {
            var orders = await _orderRepository.GetOrdersAsync();

            var ordersResponse = new List<OrderDTOResponse>();
            foreach(var order in orders)
            {
                var orderDTO = new OrderDTOResponse(
                order.Id,
                order.BuyerId,
                new List<OrderItem>(order.OrderItems)
                );
                ordersResponse.Add(orderDTO);
            }

            return ordersResponse;
        }

        public async Task<OrderDTOResponse> PostOrder(OrderDTORequest orderToPost)
        {
            var id = Guid.NewGuid();
            var order = new Order(
                id,
                (Guid)orderToPost.BuyerId!,
                (List<OrderItem>)orderToPost.OrderItems!
                );

            await _orderRepository.AddOrderAsync(order);
            await _orderRepository.SaveChangesAsync();

            var orderDTO = new OrderDTOResponse(
                order.Id,
                order.BuyerId,
                new List<OrderItem>(order.OrderItems)
                );

            return orderDTO;
        }

        public async Task<OrderDTOResponse> PutOrder(Guid id, OrderDTORequest orderToUpdate)
        {
            var order = new Order(
                id,
                (Guid)orderToUpdate.BuyerId!,
                (List<OrderItem>)orderToUpdate.OrderItems!
                );

            if (!await _orderRepository.OrderExistsAsync(id))
            {
                throw new RecourseNotFoundException();
            }

            _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChangesAsync();

            var orderDTO = new OrderDTOResponse(
                order.Id,
                order.BuyerId,
                new List<OrderItem>(order.OrderItems)
                );

            return orderDTO;
        }
    }
}
