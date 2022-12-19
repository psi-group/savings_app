using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SavingsAppContext _appContext;

        public ProductRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await _appContext.Products.AddAsync(product);

            return product;
        }

        public async Task<Product?> GetProductAsync(Guid id)
        {
            try
            {
                var res = _appContext.Products.FirstOrDefaultAsync(product => product.Id == id);
                return await res;
                
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _appContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(ISpecification<Product> spec)
        {
            
            var res = spec.Includes
                .Aggregate(_appContext.Products.AsQueryable(),
                    (current, include) => current.Include(include));

            res = spec.IncludeStrings
                .Aggregate(res,
                    (current, include) => current.Include(include));


            if (spec.IsPagingEnabled)
            {
                res = res.Skip(spec.Skip)
                             .Take(spec.Take);
            }

            if(spec.Criteria != null)
            {
                res = res.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                res = res.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                res = res.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.GroupBy != null)
            {
                res = res.GroupBy(spec.GroupBy).SelectMany(x => x);
            }

            return await res.ToListAsync();
        }

        public async Task<bool> ProductExistsAsync(Guid id)
        {
            return await _appContext.Products.AnyAsync(e => e.Id == id);
        }


        public Product RemoveProduct(Product product)
        {
            _appContext.Products.Remove(product);
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            try
            {
                _appContext.Entry(product).State = EntityState.Modified;
            }
            catch(Exception e)
            {
                return null;
            }
            

            return product;
        }

        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync();
        }

    }
}
