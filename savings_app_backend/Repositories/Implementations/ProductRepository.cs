using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Repositories.Interfaces;

namespace savings_app_backend.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly SavingsAppContext _appContext;

        public ProductRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Product> AddProduct(Product product)
        {
            _appContext.Products.Add(product);

            await _appContext.SaveChangesAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetFilteredProducts(Func<Product,
            Object>? orderBy, params Func<Product, bool>[] filters)
        {
            IEnumerable<Product> filteredProducts = null;

            if(filters.Length > 0)
            {
                filteredProducts = _appContext.Products.Where(filters[0]);
            }

            for(int i = 1; i < filters.Length; ++i)
            {
                filteredProducts = filteredProducts.Where(filters[i]);
            }

            if(orderBy != null)
                return filteredProducts.OrderBy(orderBy).ToList();
            else
            {
                return filteredProducts.ToList();
            }

        }

        public async Task<Product?> GetProduct(Guid id)
        {
            return await _appContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _appContext.Products.ToListAsync();
        }

        public async Task<bool> ProductExists(Guid id)
        {
            return await _appContext.Products.AnyAsync(e => e.Id == id);
        }

        public async Task<Product> RemoveProduct(Product product)
        {

            _appContext.Products.Remove(product);
            await _appContext.SaveChangesAsync();
            return product;
            
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _appContext.Entry(product).State = EntityState.Modified;

            await _appContext.SaveChangesAsync();

            return product;
        }
    }
}
