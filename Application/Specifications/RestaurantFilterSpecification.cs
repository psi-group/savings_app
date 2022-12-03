using Domain.Entities;

namespace Application.Specifications
{
    public class RestaurantFilterSpecification : BaseSpecification<Restaurant>
    {
        public RestaurantFilterSpecification(string? search) : base()
        {
            ApplyCriteria(restaurant => String.IsNullOrEmpty(search) ||
                (restaurant.Name.Contains(search) ||
                (!String.IsNullOrEmpty(restaurant.Description) && restaurant.Description.Contains(search))));
        }
    }
}
