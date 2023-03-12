using Application.Services.Interfaces;
using Application.Specifications;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Transactions;

namespace Application.Services.Implementations
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IFileSaver _fileSaver;
        private readonly IHttpContextAccessor _httpContext;

        public ProductService(IHttpContextAccessor httpContext,
            IProductRepository productRepository, IFileSaver fileSaver)
        {
            _productRepository = productRepository;
            _fileSaver = fileSaver;
            _httpContext = httpContext;
        }

        public async Task<IEnumerable<ProductDTOResponse>> GetFilteredProducts(List<Category> category,
            string? search, string? order)
        {
            var spec = new ProductFIlterSpecification(category, search, order);

            var products = await _productRepository.GetProductsAsync(spec);
            var productsDTO = new List<ProductDTOResponse>();
            foreach(var product in products)
            {
                var productDTO = new ProductDTOResponse(
                product.Id,
                product.Name,
                product.IsHidden,
                product.Category,
                product.RestaurantID,
                product.AmountType,
                product.AmountPerUnit,
                product.AmountOfUnits,
                product.Price,
                product.ImageUrl,
                product.ShelfLife,
                product.Description
                );

                productsDTO.Add(productDTO);
            }

            return productsDTO;
        }

        public async Task<IEnumerable<ProductDTOResponse>> GetProducts()
        {
            

            var products = await _productRepository.GetProductsAsync();

            var productsDTO = new List<ProductDTOResponse>();
            foreach (var product in products)
            {
                var productDTO = new ProductDTOResponse(
                product.Id,
                product.Name,
                product.IsHidden,
                product.Category,
                product.RestaurantID,
                product.AmountType,
                product.AmountPerUnit,
                product.AmountOfUnits,
                product.Price,
                product.ImageUrl,
                product.ShelfLife,
                product.Description
                );

                productsDTO.Add(productDTO);
            }

            return productsDTO;
        }

        public async Task<ProductDTOResponse> GetProduct(Guid id)
        {
            var product = await _productRepository.GetProductAsync(id);

            if(product == null)
            {
                throw new RecourseNotFoundException("product with this id does not exist");
            }
            else
            {
                var productDTO = new ProductDTOResponse(
                product.Id,
                product.Name,
                product.IsHidden,
                product.Category,
                product.RestaurantID,
                product.AmountType,
                product.AmountPerUnit,
                product.AmountOfUnits,
                product.Price,
                product.ImageUrl,
                product.ShelfLife,
                product.Description
                );

                return productDTO;
            }
        }
        public async Task<ProductDTOResponse> PostProduct(ProductDTORequest productToPost)
        {

            if (productToPost.RestaurantID !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity!).FindFirst("Id")!.Value))
                throw new InvalidIdentityException("you are unauthorized to create this resource");

            var id = Guid.NewGuid();

            var product = new Product(
                id,
                productToPost.Name!,
                (Category)productToPost.Category!,
                (Guid)productToPost.RestaurantID,
                null,
                (AmountType)productToPost.AmountType!,
                (float)productToPost.AmountPerUnit!,
                (int)productToPost.AmountOfUnits!,
                (float)productToPost.Price!,
                productToPost.Image == null ? null :
                "https://cityfood.blob.core.windows.net/cityfoodproductimages/" + id + ".jpg",
                (DateTime)productToPost.ShelfLife!,
                productToPost.Description);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                await _productRepository.AddProductAsync(product);
                await _productRepository.SaveChangesAsync();


                if (productToPost.Image != null)
                {
                    Task saveImageTask = _fileSaver.SaveImage
                    (productToPost.Image, product.Id.ToString(), true);
                    await saveImageTask;
                }
                

                scope.Complete();
            }

            

            var productDTO = new ProductDTOResponse(
                product.Id,
                product.Name,
                product.IsHidden,
                product.Category,
                product.RestaurantID,
                product.AmountType,
                product.AmountPerUnit,
                product.AmountOfUnits,
                product.Price,
                product.ImageUrl,
                product.ShelfLife,
                product.Description
                );

            return productDTO;
        }
    }
}
