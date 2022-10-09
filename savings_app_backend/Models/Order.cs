namespace savings_app_backend.Models
{
    public class Order
    {
        public string buyerId { get; set; }
        public string orderId { get; set; }
        public string sellerId { get; set; }

        public int orderStatus { get; set; }

        public string productId { get; set; }
    }
}
