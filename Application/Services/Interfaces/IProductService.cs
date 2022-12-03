using Domain.DTOs.Request;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetFilteredProducts(List<Category> category, string? search, string? order);

        Task<Product> GetProduct(Guid id);

        Task<Product> PutProduct(Guid id, ProductDTORequest product);

        Task<Product> DeleteProduct(Guid id);

        Task<Product> PostProduct(ProductDTORequest product);

    }
}
