using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Extention;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.Services.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Security.Claims;
using savings_app_backend.EmailSender;
using savings_app_backend.Repositories.Interfaces;
using System.Security.Cryptography.X509Certificates;
using savings_app_backend.SavingToFile;

namespace savings_app_backend.Services.Implementations
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserAuthRepository _userAuthRepository;

        private readonly IFileSaver _fileSaver;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IEmailSender _emailSender;

        public ProductService(IWebHostEnvironment webHostEnvironment,
            IConfiguration config, IHttpContextAccessor httpContext, IEmailSender emailSender,
            IProductRepository productRepository,
            IUserAuthRepository userAuthRepository, IRestaurantRepository restaurantRepository, IFileSaver fileSaver)
        {
            _restaurantRepository = restaurantRepository;
            _userAuthRepository = userAuthRepository;
            _productRepository = productRepository;

            _fileSaver = fileSaver;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
            _httpContext = httpContext;
            _emailSender = emailSender;
            _userAuthRepository = userAuthRepository;
        }

        public async Task<Product> DeleteProduct(Guid id)
        {
            var product = await _productRepository.GetProduct(id);

            if (product == null)
            {
                throw new RecourseNotFoundException();
            }

            if (product.RestaurantID !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();


            return await _productRepository.RemoveProduct(product);
        }

        public async Task<IEnumerable<Product>> GetFilteredProducts(List<Category> category,
            string? search, string? order)
        {

            Func<Product, Object> sortingOrderDelegate;
            
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
            

            return await products;
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var product = await _productRepository.GetProduct(id);

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
            return await _productRepository.GetProducts();
        }


        public async Task<Product> PostProduct(Product product)
        {
            if (product.RestaurantID !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity!).FindFirst("Id")!.Value))
                throw new InvalidIdentityException();
            
            product.Id = Guid.NewGuid();

            var imageName = product.Id.ToString();

            Task saveImageTask = _fileSaver.SaveImage(product.ImageFile, imageName,
                    _config["ImageStorage:ImageFoldersPaths:ProductImages"],
                    _config["ImageStorage:ImageFoldersPaths:ImageExtention"]);

            product.ImageName = imageName;

            await saveImageTask;
            await _productRepository.AddProduct(product);
            
            return product;
        }

        

        public async Task<Product> Buy(Guid id, int amount)
        {
            // before repository pattern
            /*var product = _context.Products
                .Include(product => product.Restaurant)
                .Include(product => product.Restaurant.UserAuth)
                .GetById(id);
            */

            var product = await _productRepository.GetProduct(id);
            var restaurant = _restaurantRepository.GetRestaurant(product.RestaurantID);
            var userAuth = _userAuthRepository.GetUserAuth(id);


            product.ProductSold += _emailSender.NotifyRestaurantSoldProduct;

            product.ProductSoldOut += _emailSender.NotifyRestaurantSoldOutProduct;

            product.ReduceAmount(amount);

            await _productRepository.UpdateProduct(product);

            return product;
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

            if (!await ProductExistsAsync(id))
            {
                throw new RecourseNotFoundException();
            }

            await _productRepository.UpdateProduct(product);

            
            return product;
        }

        private async Task<bool> ProductExistsAsync(Guid id)
        {

            return await _productRepository.ProductExists(id);
        }
    }
}
