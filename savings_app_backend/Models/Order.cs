namespace savings_app_backend.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid buyerId { get; set; }
        public Guid sellerId { get; set; }

        public Guid pickupTimeId { get; set; }

        public int orderStatus { get; set; }

        public string productId { get; set; }
    }
}
