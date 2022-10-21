using savings_app_backend.Models.Enums;

namespace savings_app_backend.Models.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid buyerId { get; set; }
        public Buyer Buyer { get; set; }

        public Guid sellerId { get; set; }
        public Restaurant Restaurant { get; set; }
        

        public Guid pickupTimeId { get; set; }

        public OrderStatus Status { get; set; }

        public string productId { get; set; }
    }
}
