using Domain.Entities;
using Domain.Events;

namespace Domain.Interfaces
{
    public interface IEmailSender
    {
        public void NotifyRestaurantSoldProduct(Object? sender, ProductSoldEventArgs args);

        public void NotifyRestaurantSoldOutProduct(Object? sender, ProductSoldOutEventArgs args);
    }
}
