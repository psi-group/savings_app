using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using System.Transactions;

namespace Application.Services.Implementations
{
    public class ShopService : IShopService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPickupRepository _pickupRepository;
        private readonly IEmailSender _emailSender;

        public ShopService(IProductRepository productRepository, IPickupRepository pickupRepository,
            IOrderRepository orderRepository, IEmailSender emailSender)
        {
            _productRepository = productRepository;
            _pickupRepository = pickupRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
        }

        public async Task<Order> Checkout(CheckoutDTORequest checkout)
        {
            var orderId = Guid.NewGuid();
            var orderItemList = new List<OrderItem>();
            /*var productsToBuy = new List<Product>();
            var pickupsToBook = new List<Pickup>();

            foreach (var product in checkout.productsToBuy)
            {
                var productBeingBought = await _productRepository.GetProductAsync(product.Id);
                productBeingBought.ProductSold += _emailSender.NotifyRestaurantSoldProduct;
                productBeingBought.ProductSoldOut += _emailSender.NotifyRestaurantSoldOutProduct;

                var pickup = await _pickupRepository.GetPickupAsync(product.PickupId);

                var orderItem = new OrderItem(
                    Guid.NewGuid(),
                    orderId,
                    product.Id,
                    product.PickupId,
                    product.Amount,
                    productBeingBought.Price,
                    OrderItemStatus.AwaitingPickup
                    );

                productsToBuy.Add(productBeingBought);
                pickupsToBook.Add(pickup);
                orderItemList.Add(orderItem);
            }*/

            /*using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                foreach(var product in checkout.productsToBuy)
                {
                    var productBeingBought = await _productRepository.GetProductAsync(product.Id);
                    productBeingBought.ProductSold += _emailSender.NotifyRestaurantSoldProduct;
                    productBeingBought.ProductSoldOut += _emailSender.NotifyRestaurantSoldOutProduct;
                    productBeingBought.Buy(product.Amount, checkout.buyerId);

                    var boughtProduct = _productRepository.UpdateProduct(productBeingBought);
                    var pickup = await _pickupRepository.GetPickupAsync(product.PickupId);
                    pickup.Book();

                    var orderItem = new OrderItem(
                        Guid.NewGuid(),
                        orderId,
                        product.Id,
                        product.PickupId,
                        product.Amount,
                        productBeingBought.Price,
                        OrderItemStatus.AwaitingPickup
                        );

                    orderItemList.Add(orderItem);
                }

                var order = new Order(orderId, checkout.buyerId, orderItemList);

                scope.Complete();
            }*/

            foreach (var product in checkout.productsToBuy)
            {
                var productBeingBought = await _productRepository.GetProductAsync(product.Id);
                
                productBeingBought.ProductSold += _emailSender.NotifyRestaurantSoldProduct;
                
                productBeingBought.ProductSoldOut += _emailSender.NotifyRestaurantSoldOutProduct;
                
                productBeingBought.Buy(product.Amount, checkout.buyerId);


                productBeingBought.ProductSold -= _emailSender.NotifyRestaurantSoldProduct;
                productBeingBought.ProductSoldOut -= _emailSender.NotifyRestaurantSoldOutProduct;

                var boughtProduct = _productRepository.UpdateProduct(productBeingBought);

                var pickup = await _pickupRepository.GetPickupAsync(product.PickupId);
                pickup.Book();

                var orderItem = new OrderItem(
                    Guid.NewGuid(),
                    orderId,
                    product.Id,
                    product.PickupId,
                    product.Amount,
                    productBeingBought.Price,
                    OrderItemStatus.AwaitingPickup
                    );

                orderItemList.Add(orderItem);
            }

            var order = new Order(orderId, checkout.buyerId, orderItemList);
            await _orderRepository.AddOrderAsync(order);
            try
            {
                await _orderRepository.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return null;
            }
            

            return order;
        }
    }
}
