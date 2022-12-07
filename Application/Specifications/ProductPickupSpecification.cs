using Domain.Entities;

namespace Application.Specifications
{
    public class ProductPickupSpecification : BaseSpecification<Pickup>
    {
        public ProductPickupSpecification(Guid productId) : base()
        {
            ApplyCriteria(pickup => pickup.ProductId == productId);
        }
    }
}
