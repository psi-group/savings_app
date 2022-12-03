using Application.Services.Interfaces;
using Application.Specifications;
using Domain.DTOs.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;


namespace Application.Services.Implementations
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        private readonly IFileSaver _fileSaver;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IEmailSender _emailSender;

        public ProductService(IWebHostEnvironment webHostEnvironment,
            IConfiguration config, IHttpContextAccessor httpContext, IEmailSender emailSender,
            IProductRepository productRepository,
            IRestaurantRepository restaurantRepository, IFileSaver fileSaver)
        {
            _restaurantRepository = restaurantRepository;
            _productRepository = productRepository;
            _fileSaver = fileSaver;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
            _httpContext = httpContext;
            _emailSender = emailSender;
        }

        public async Task<Product> DeleteProduct(Guid id)
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
            return product;
        }

        public async Task<IEnumerable<Product>> GetFilteredProducts(List<Category> category,
            string? search, string? order)
        {
            var spec = new ProductFIlterSpecification(category, search, order);

            return await _productRepository.GetProductsAsync(spec);
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var product = await _productRepository.GetProductAsync(id);

            if(product == null)
            {
                throw new RecourseNotFoundException();
            }
            else
            {
                return product;
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetProductsAsync();
        }


        public async Task<Product> PostProduct(ProductDTORequest productToPost)
        {

            //throw new NotImplementedException();

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

            Task saveImageTask = _fileSaver.SaveImage(productToPost.Image, product.ImageName,
                    _webHostEnvironment.ContentRootPath + _config["ImageStorage:ImageFoldersPaths:ProductImages"],
                    _config["ImageStorage:ImageExtention"]);

            await saveImageTask;
            await _productRepository.AddProductAsync(product);
            await _productRepository.SaveChangesAsync();
            return product;
        }

        

        public async Task<Product> Buy(Guid id, int amount)
        {
            throw new NotImplementedException();
            // before repository pattern
            /*var product = _context.Products
                .Include(product => product.Restaurant)
                .Include(product => product.Restaurant.UserAuth)
                .GetById(id);
            */
            /*
            var product = await _productRepository.GetProduct(id);
            var restaurant = await _restaurantRepository.GetRestaurant(product.RestaurantID);
            var userAuth = await _userAuthRepository.GetUserAuth(restaurant.UserAuthId);
            restaurant.UserAuth = userAuth;
            product.Restaurant = restaurant;
            

            product.ProductSold += _emailSender.NotifyRestaurantSoldProduct;

            product.ProductSoldOut += _emailSender.NotifyRestaurantSoldOutProduct;

            product.ReduceAmount(amount);

            await _productRepository.UpdateProduct(product);

            product.ProductSold -= _emailSender.NotifyRestaurantSoldProduct;

            product.ProductSoldOut -= _emailSender.NotifyRestaurantSoldOutProduct;

            product.Restaurant = null;

            return product;*/
        }

        public async Task<Product> PutProduct(Guid id, ProductDTORequest productToChange)
        {

            var c = productToChange.Name;

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


            return product;
        }
    }
}
