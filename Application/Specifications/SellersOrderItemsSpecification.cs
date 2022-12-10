using Domain.Entities;
using Domain.Entities.OrderAggregate;

namespace Application.Specifications
{
    public class SellersOrderItemsSpecification : BaseSpecification<Order>
    {
        public SellersOrderItemsSpecification(Guid sellerId) : base()
        {
            AddInclude(order => order.OrderItems.Where(orderItem => orderItem.Product.RestaurantID == sellerId));
            AddInclude("OrderItems.Product");
            //AddInclude(order => order.OrderItems);
            ApplyCriteria(order => order.OrderItems.FirstOrDefault(orderItem => orderItem.Product.RestaurantID == sellerId) != null);
        }
    }
}
