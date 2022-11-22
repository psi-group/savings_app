using savings_app_backend.Models.Entities;

namespace savings_app_backend.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product?> GetProduct(Guid id);
        public Task<Product> RemoveProduct(Product product);

        public Task<IEnumerable<Product>> GetProducts();

        public Task<Product> UpdateProduct(Product product);
        public Task<Product> AddProduct(Product product);

        public Task<bool> ProductExists(Guid id);

        public Task<IEnumerable<Product>> GetFilteredProducts(Func<Product, Object>? orderBy, params Func<Product, bool>[] filters);

    }
}
