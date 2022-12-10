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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;

        public ProductService(IWebHostEnvironment webHostEnvironment,
            IConfiguration config, IHttpContextAccessor httpContext,
            IProductRepository productRepository, IFileSaver fileSaver)
        {
            _productRepository = productRepository;
            _fileSaver = fileSaver;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
            _httpContext = httpContext;
        }

        public async Task<ProductDTOResponse> DeleteProduct(Guid id)
        {
            var product = await _productRepository.GetProductAsync(id);

            if (product == null)
            {
                throw new RecourseNotFoundException();
            }

            if (product.RestaurantID !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();


            _productRepository.RemoveProduct(product);
            await _productRepository.SaveChangesAsync();

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
                product.ImageName,
                product.ShelfLife,
                product.Description
                );

            return productDTO;
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
                product.ImageName,
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
                throw new RecourseNotFoundException();
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
                product.ImageName,
                product.ShelfLife,
                product.Description
                );

                return productDTO;
            }
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
                product.ImageName,
                product.ShelfLife,
                product.Description
                );

                productsDTO.Add(productDTO);
            }

            return productsDTO;
        }


        public async Task<ProductDTOResponse> PostProduct(ProductDTORequest productToPost)
        {

            if (productToPost.RestaurantID !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity!).FindFirst("Id")!.Value))
                throw new InvalidIdentityException();

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
                id.ToString(),
                (DateTime)productToPost.ShelfLife!,
                productToPost.Description);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                await _productRepository.AddProductAsync(product);
                await _productRepository.SaveChangesAsync();


                if (productToPost.Image != null)
                {
                    Task saveImageTask = _fileSaver.SaveImage
                    (productToPost.Image, product.ImageName, true);
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
                product.ImageName,
                product.ShelfLife,
                product.Description
                );

            return productDTO;
        }
        public async Task<ProductDTOResponse> PutProduct(Guid id, ProductDTORequest productToChange)
        {
            if (productToChange.RestaurantID !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();

            if (!await _productRepository.ProductExistsAsync(id))
            {
                throw new RecourseNotFoundException();
            }


            var product = new Product(
                id,
                productToChange.Name!,
                (Category)productToChange.Category!,
                (Guid)productToChange.RestaurantID,
                null,
                (AmountType)productToChange.AmountType!,
                (float)productToChange.AmountPerUnit!,
                (int)productToChange.AmountOfUnits!,
                (float)productToChange.Price!,
                id.ToString(),
                (DateTime)productToChange.ShelfLife!,
                productToChange.Description);

            _productRepository.UpdateProduct(product);
            await _productRepository.SaveChangesAsync();

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
                product.ImageName,
                product.ShelfLife,
                product.Description
                );

            return productDTO;
        }
    }
}
