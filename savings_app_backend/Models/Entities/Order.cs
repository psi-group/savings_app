using savings_app_backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace savings_app_backend.Models.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid BuyerId { get; set; }
        public Buyer? Buyer { get; set; }

        public Guid SellerId { get; set; }
        public Restaurant? Restaurant { get; set; }
        
        public Pickup? Pickup { get; set; }
        public Guid PickupId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus Status { get; set; }

        public string? productId { get; set; }
    }
}
