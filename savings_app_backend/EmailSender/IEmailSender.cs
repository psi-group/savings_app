using savings_app_backend.Events;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.EmailSender
{
    public interface IEmailSender
    {
        public void NotifyRestaurantSoldProduct(Object sender, ProductSoldEventArgs args);

        public void NotifyRestaurantSoldOutProduct(Product sender, string sellerEmail);


    }
}
