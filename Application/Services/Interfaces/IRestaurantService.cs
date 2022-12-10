using Domain.DTOs.Request;
using Domain.DTOs.Response;

namespace Application.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDTOResponse>> GetRestaurants();
        Task<IEnumerable<RestaurantDTOResponse>> GetFilteredRestaurants(string? search);

        Task<RestaurantDTOResponse> GetRestaurant(Guid id);
        Task<RestaurantPrivateDTOResponse> GetRestaurantPrivate(Guid id);

        Task<RestaurantDTOResponse> PutRestaurant(Guid id, RestaurantDTORequest restaurant);

        Task<RestaurantDTOResponse> DeleteRestaurant(Guid id);

        Task<RestaurantDTOResponse> PostRestaurant(RestaurantDTORequest restaurant);

    }
}
