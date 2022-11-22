using savings_app_backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace savings_app_backend.Models.Entities
{
    public class Order
    {

        public Order()
        {

        }

        public Order(Guid id, Guid buyerId, Guid sellerId, Guid pickupId, OrderStatus orderStatus, Guid productId)
        {
            Id = id;
            BuyerId = buyerId;
            SellerId = sellerId;
            PickupId = pickupId;
            OrderStatus = orderStatus;
            ProductId = productId;
        }


        public Guid Id { get; set; }

        public Guid BuyerId { get; set; }
        public Buyer Buyer { get; set; }

        public Guid SellerId { get; set; }
        public Restaurant Restaurant { get; set; }
        
        public Pickup Pickup { get; set; }
        public Guid PickupId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus OrderStatus { get; set; }

        public Guid ProductId { get; set; }
    }
}
