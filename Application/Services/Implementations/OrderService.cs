using Application.Services.Interfaces;
using Application.Specifications;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpContext;
        public OrderService(IOrderRepository orderRepository, IHttpContextAccessor httpContext)
        {
            _orderRepository = orderRepository;
            _httpContext = httpContext;
        }

        public async Task<OrderDTOResponse> DeleteOrder(Guid id)
        {
            var order = await _orderRepository.GetOrderAsync(id);
            if (order == null)
            {
                throw new RecourseNotFoundException("order with this id does not exist");
            }

            if (order.BuyerId !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException("you are unauthorized to delete this resource");

            _orderRepository.RemoveOrder(order);
            await _orderRepository.SaveChangesAsync();

            var orderItemListDTO = new List<OrderItemDTOResponse>();

            foreach (var orderItem in order.OrderItems)
            {
                var orderItemDTO = new OrderItemDTOResponse(
                    orderItem.Id,
                    orderItem.OrderId,
                    orderItem.ProductId,
                    orderItem.PickupId,
                    orderItem.UnitsOrdered,
                    orderItem.Price,
                    orderItem.OrderItemStatus
                    );
                orderItemListDTO.Add(orderItemDTO);
            }

            var orderDTO = new OrderDTOResponse(
                order.Id,
                order.BuyerId,
                orderItemListDTO
                );

            return orderDTO;
        }

        public async Task<IEnumerable<OrderDTOResponse>> GetBuyersOrders(Guid buyerId)
        {
            if (buyerId !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException("you are unauthorized to access this resource");

            var spec = new BuyersOrdersSpecification(buyerId);

            var orders = await _orderRepository.GetOrdersAsync(spec);

            var ordersResponse = new List<OrderDTOResponse>();
            foreach (var order in orders)
            {
                var orderItemListDTO = new List<OrderItemDTOResponse>();

                foreach (var orderItem in order.OrderItems)
                {
                    var orderItemDTO = new OrderItemDTOResponse(
                        orderItem.Id,
                        orderItem.OrderId,
                        orderItem.ProductId,
                        orderItem.PickupId,
                        orderItem.UnitsOrdered,
                        orderItem.Price,
                        orderItem.OrderItemStatus
                        );
                    orderItemListDTO.Add(orderItemDTO);
                }

                var orderResponse = new OrderDTOResponse(
                    order.Id,
                    order.BuyerId,
                    orderItemListDTO
                );
                ordersResponse.Add(orderResponse);
            }
            return ordersResponse;
        }

        public async Task<OrderDTOResponse> GetOrder(Guid id)
        {

            var order = await _orderRepository.GetOrderAsync(id);

            if (order.BuyerId !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException("you are unauthorized to access this resource");


            if (order == null)
            {
                throw new RecourseNotFoundException("order with this id does not exist");
            }
            else
            {
                var orderItemListDTO = new List<OrderItemDTOResponse>();

                foreach (var orderItem in order.OrderItems)
                {
                    var orderItemDTO = new OrderItemDTOResponse(
                        orderItem.Id,
                        orderItem.OrderId,
                        orderItem.ProductId,
                        orderItem.PickupId,
                        orderItem.UnitsOrdered,
                        orderItem.Price,
                        orderItem.OrderItemStatus
                        );
                    orderItemListDTO.Add(orderItemDTO);
                }

                var orderDTO = new OrderDTOResponse(
                order.Id,
                order.BuyerId,
                orderItemListDTO
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
                var orderItemListDTO = new List<OrderItemDTOResponse>();

                foreach (var orderItem in order.OrderItems)
                {
                    var orderItemDTO = new OrderItemDTOResponse(
                        orderItem.Id,
                        orderItem.OrderId,
                        orderItem.ProductId,
                        orderItem.PickupId,
                        orderItem.UnitsOrdered,
                        orderItem.Price,
                        orderItem.OrderItemStatus
                        );
                    orderItemListDTO.Add(orderItemDTO);
                }

                var orderDTO = new OrderDTOResponse(
                order.Id,
                order.BuyerId,
                orderItemListDTO
                );
                ordersResponse.Add(orderDTO);
            }

            return ordersResponse;
        }

        public async Task<IEnumerable<OrderItemDTOResponse>> GetSellersOrderItems(Guid sellerId)
        {
            if (sellerId !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException("you are unauthorized to access this resource");

            var spec = new SellersOrderItemsSpecification(sellerId);

            var orders = await _orderRepository.GetOrdersAsync(spec);

            var orderItemsResponse = new List<OrderItemDTOResponse>();
            foreach (var order in orders)
            {
                foreach(var orderItem in order.OrderItems)
                {
                    var orderItemResponse = new OrderItemDTOResponse(
                        orderItem.Id,
                        orderItem.OrderId,
                        orderItem.ProductId,
                        orderItem.PickupId,
                        orderItem.UnitsOrdered,
                        orderItem.Price,
                        orderItem.OrderItemStatus
                );
                    orderItemsResponse.Add(orderItemResponse);
                }
                
            }
            return orderItemsResponse;
        }

        public async Task<OrderDTOResponse> PostOrder(OrderDTORequest orderToPost)
        {
            var id = Guid.NewGuid();
            var order = new Order(
                id,
                (Guid)orderToPost.BuyerId!,
                orderToPost.OrderItems!
                );

            await _orderRepository.AddOrderAsync(order);
            await _orderRepository.SaveChangesAsync();

            var orderItemListDTO = new List<OrderItemDTOResponse>();

            foreach (var orderItem in order.OrderItems)
            {
                var orderItemDTO = new OrderItemDTOResponse(
                    orderItem.Id,
                    orderItem.OrderId,
                    orderItem.ProductId,
                    orderItem.PickupId,
                    orderItem.UnitsOrdered,
                    orderItem.Price,
                    orderItem.OrderItemStatus
                    );
                orderItemListDTO.Add(orderItemDTO);
            }

            var orderDTO = new OrderDTOResponse(
                order.Id,
                order.BuyerId,
                orderItemListDTO
                );

            return orderDTO;
        }

        public async Task<OrderDTOResponse> PutOrder(Guid id, OrderDTORequest orderToUpdate)
        {
            var order = new Order(
                id,
                (Guid)orderToUpdate.BuyerId!,
                orderToUpdate.OrderItems!
                );

            if (!await _orderRepository.OrderExistsAsync(id))
            {
                throw new RecourseNotFoundException("order with this id does not exist");
            }

            var orderCheck = await _orderRepository.GetOrderAsync(id);

            if (orderCheck.BuyerId !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException("you are unauthorized to access this resource");

            _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChangesAsync();

            var orderItemListDTO = new List<OrderItemDTOResponse>();

            foreach (var orderItem in order.OrderItems)
            {
                var orderItemDTO = new OrderItemDTOResponse(
                    orderItem.Id,
                    orderItem.OrderId,
                    orderItem.ProductId,
                    orderItem.PickupId,
                    orderItem.UnitsOrdered,
                    orderItem.Price,
                    orderItem.OrderItemStatus
                    );
                orderItemListDTO.Add(orderItemDTO);
            }

            var orderDTO = new OrderDTOResponse(
                order.Id,
                order.BuyerId,
                orderItemListDTO
                );

            return orderDTO;
        }
    }
}
