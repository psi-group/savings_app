namespace savings_app_backend.Events
{
    public class ProductSoldEventArgs
    {
        public int Amount { get; set; }
        public string SellerEmail { get; set; }

        public ProductSoldEventArgs(int amount, string sellerEmail)
        {
            Amount = amount;
            SellerEmail = sellerEmail;
        }
    }
}
