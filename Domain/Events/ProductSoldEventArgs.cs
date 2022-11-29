namespace Domain.Events
{
    public class ProductSoldEventArgs
    {
        public int Amount { get; set; }

        public string BuyerEmail { get; set; }

        public ProductSoldEventArgs(int amount, string buyerEmail)
        {
            Amount = amount;
            BuyerEmail = buyerEmail;
        }
    }
}
