using Application.Services.Interfaces;
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
            throw new NotImplementedException();
            /*Func<Product, Object> sortingOrderDelegate;
            
            switch (order)
            {
                case null:
                    sortingOrderDelegate = null;
                    break;
                case "by_id":
                    sortingOrderDelegate = (product) => product.Id;
                    break;
                case "by_name":
                    sortingOrderDelegate = (product) => product.Name;
                    break;
                case "by_price":
                    sortingOrderDelegate = (product) => product.Price;
                    break;
                default:
                    throw new InvalidRequestArgumentsException();
            }

            var products = _productRepository.GetFilteredProducts(sortingOrderDelegate,
                product => SearchUtility.SearchObject(
                    product, search, (product) => product.Name, (product) => product.Description),
                product => (category.Count() == 0 || category.Contains(product.Category))
                    );
            

            return await products;*/
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
            if (productToPost.RestaurantID !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity!).FindFirst("Id")!.Value))
                throw new InvalidIdentityException();
            
            var id = Guid.NewGuid();

            var product = new Product(
                id,
                productToPost.Name,
                productToPost.Category,
                productToPost.RestaurantID,
                productToPost.AmountType,
                productToPost.AmountPerUnit,
                productToPost.AmountOfUnits,
                productToPost.Price,
                id.ToString(),
                productToPost.ShelfLife,
                productToPost.Description);

            

            Task saveImageTask = _fileSaver.SaveImage(productToPost.Image, product.ImageName,
                    _webHostEnvironment.WebRootPath + _config["ImageStorage:ImageFoldersPaths:ProductImages"],
                    _config["ImageStorage:ImageFoldersPaths:ImageExtention"]);

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

        public async Task<Product> PutProduct(Guid id, Product product)
        {
            if (product.RestaurantID !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();

            if (id != product.Id)
            {
                throw new InvalidRequestArgumentsException();
            }

            if (!await _productRepository.ProductExistsAsync(id))
            {
                throw new RecourseNotFoundException();
            }

            _productRepository.UpdateProduct(product);
            await _productRepository.SaveChangesAsync();


            return product;
        }
    }
}
