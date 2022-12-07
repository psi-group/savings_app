namespace Domain.Events
{
    public class ProductSoldEventArgs
    {
        public int Amount { get; set; }

        public Guid BuyerId { get; set; }

        public ProductSoldEventArgs(int amount, Guid buyerId)
        {
            Amount = amount;
            BuyerId = buyerId;
        }
    }
}
