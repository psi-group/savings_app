using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SavingsAppContext _appContext;

        public ProductRepository(SavingsAppContext appContext)
        {
            _appContext = appContext;
        }

        public Product AddProduct(Product product)
        {
            _appContext.Products.Add(product);

            return product;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            await _appContext.Products.AddAsync(product);

            return product;
        }


        public Product? GetProduct(Guid id)
        {
            return _appContext.Products.Find(id);
        }
        public async Task<Product?> GetProductAsync(Guid id)
        {
            return await _appContext.Products.FindAsync(id);
        }



        public IEnumerable<Product> GetProducts()
        {
            return _appContext.Products.ToList();
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _appContext.Products.ToListAsync();
        }


        public IEnumerable<Product> GetProducts(ISpecification<Product> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_appContext.Products.AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                            .Where(spec.Criteria)
                            .OrderBy(spec.OrderBy)
                            .ToList();
        }
        public async Task<IEnumerable<Product>> GetProductsAsync(ISpecification<Product> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_appContext.Products.AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));


            var a = _appContext.Products.ToList();
            // return the result of the query using the specification's criteria expression
            return await secondaryResult
                            //.Where(spec.Criteria)
                            .ToListAsync();
        }



        public bool ProductExists(Guid id)
        {
            return _appContext.Products.Any(e => e.Id == id);
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
            _appContext.Entry(product).State = EntityState.Modified;

            return product;
        }

        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _appContext.SaveChanges();
        }
    }
}
