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
            try
            {
                //var product = /*await*/ _appContext.Products.FindAsync(id);
                //return await _appContext.Products.FindAsync(id);
                var res = _appContext.Products.FirstOrDefaultAsync(product => product.Id == id);
                return await res;
                
            }
            catch(Exception ex)
            {
                return null;
            }
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
            var res = spec.Includes
                .Aggregate(_appContext.Products.AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            res = spec.IncludeStrings
                .Aggregate(res,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression


            if (spec.IsPagingEnabled)
            {
                res = res.Skip(spec.Skip)
                             .Take(spec.Take);
            }

            if (spec.Criteria != null)
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

            return res.ToList();
        }


        public async Task<IEnumerable<Product>> GetProductsAsync(ISpecification<Product> spec)
        {
            
            // fetch a Queryable that includes all expression-based includes
            var res = spec.Includes
                .Aggregate(_appContext.Products.AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            res = spec.IncludeStrings
                .Aggregate(res,
                    (current, include) => current.Include(include));


            // return the result of the query using the specification's criteria expression


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

        public void SaveChanges()
        {
            _appContext.SaveChanges();
        }
    }
}
