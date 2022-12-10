using Domain.Entities;
using Domain.Entities.OrderAggregate;

namespace Application.Specifications
{
    public class BuyersOrdersSpecification : BaseSpecification<Order>
    {
        public BuyersOrdersSpecification(Guid buyerId) : base()
        {
            AddInclude(order => order.OrderItems);
            ApplyCriteria(order => order.BuyerId == buyerId);
        }
    }
}
