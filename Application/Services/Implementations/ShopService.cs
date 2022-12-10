using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Transactions;

namespace Application.Services.Implementations
{
    public class ShopService : IShopService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPickupRepository _pickupRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContext;

        public ShopService(IProductRepository productRepository, IPickupRepository pickupRepository,
            IOrderRepository orderRepository, IEmailSender emailSender, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _productRepository = productRepository;
            _pickupRepository = pickupRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
        }

        public async Task<OrderDTOResponse> Checkout(CheckoutDTORequest checkout)
        {
            if (checkout.buyerId !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext!.User.Identity!).
                FindFirst("Id")!.Value))
                throw new InvalidIdentityException();

            var orderId = Guid.NewGuid();
            var orderItemList = new List<OrderItem>();
            

            foreach (var product in checkout.productsToBuy!)
            {
                var productBeingBought = await _productRepository.GetProductAsync((Guid)product.Id!);
                
                productBeingBought.ProductSold += _emailSender.NotifyRestaurantSoldProduct;
                
                productBeingBought.ProductSoldOut += _emailSender.NotifyRestaurantSoldOutProduct;
                
                productBeingBought.Buy((int)product.Amount!, (Guid)checkout.buyerId!);


                productBeingBought.ProductSold -= _emailSender.NotifyRestaurantSoldProduct;
                productBeingBought.ProductSoldOut -= _emailSender.NotifyRestaurantSoldOutProduct;

                var boughtProduct = _productRepository.UpdateProduct(productBeingBought);

                var pickup = await _pickupRepository.GetPickupAsync((Guid)product.PickupId!);
                pickup.Book();

                var orderItem = new OrderItem(
                    Guid.NewGuid(),
                    orderId,
                    (Guid)product.Id,
                    (Guid)product.PickupId,
                    (int)product.Amount,
                    productBeingBought.Price,
                    OrderItemStatus.AwaitingPickup
                    );

                orderItemList.Add(orderItem);
            }

            var order = new Order(orderId, (Guid)checkout.buyerId!, orderItemList);
            await _orderRepository.AddOrderAsync(order);


            await _orderRepository.SaveChangesAsync();


            
            var orderItemListDTO = new List<OrderItemDTOResponse>();

            foreach(var orderItem in orderItemList)
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

            return new OrderDTOResponse(
                order.Id,
                order.BuyerId,
                orderItemListDTO
                );
        }
    }
}
