using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; private set; }
        public Order? Order { get; private set; }

        public Guid ProductId { get; private set; }
        public Product? Product { get; private set; }

        public Guid PickupId { get; private set; }
        public Pickup? Pickup { get; private set; }

        public int UnitsOrdered { get; private set; }

        public float Price { get; private set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(OrderItemStatus))]
        public OrderItemStatus OrderItemStatus { get; set; }

        public OrderItem(Guid id, Guid orderId, Guid productId, Guid pickupId, int unitsOrdered,
            float price, OrderItemStatus orderItemStatus)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            PickupId = pickupId;
            UnitsOrdered = unitsOrdered;
            Price = price;
            OrderItemStatus = orderItemStatus;
        }

        public OrderItem()
        {

        }
    }
}
