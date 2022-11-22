using Microsoft.AspNetCore.Mvc;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;

namespace savings_app_backend.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetFilteredProducts(List<Category> category, string? search, string? order);

        Task<Product> GetProduct(Guid id);

        Task<Product> Buy(Guid id, int amount);

        Task<Product> PutProduct(Guid id, Product product);

        Task<Product> DeleteProduct(Guid id);

        Task<Product> PostProduct(Product product);

    }
}
