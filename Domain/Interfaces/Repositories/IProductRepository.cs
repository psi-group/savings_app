using Domain.Entities;
using Domain.Interfaces.Specifications;

namespace Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        public Task<Product?> GetProductAsync(Guid id);

        public Task<IEnumerable<Product>> GetProductsAsync();

        public Task<IEnumerable<Product>> GetProductsAsync(ISpecification<Product> spec);

        public Product RemoveProduct(Product product);

        public Product UpdateProduct(Product product);

        public Task<Product> AddProductAsync(Product product);

        public Task<bool> ProductExistsAsync(Guid id);

        public Task SaveChangesAsync();
    }
}
