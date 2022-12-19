using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Enums;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTOResponse>> GetFilteredProducts(List<Category> category, string? search, string? order);

        Task<ProductDTOResponse> GetProduct(Guid id);

        Task<ProductDTOResponse> PostProduct(ProductDTORequest product);

    }
}
