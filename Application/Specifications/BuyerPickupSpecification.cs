using Domain.Entities;

namespace Application.Specifications
{
    public class BuyerPickupSpecification : BaseSpecification<Pickup>
    {
        public BuyerPickupSpecification(Guid buyerId) : base()
        {
            AddInclude(pickup => pickup.OrderItem);
            AddInclude(pickup => pickup.OrderItem.Order);
            ApplyCriteria(pickup => pickup.OrderItem.Order.BuyerId == buyerId);
        }
    }
}
