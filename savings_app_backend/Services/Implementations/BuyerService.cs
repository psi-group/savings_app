using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using savings_app_backend.Exceptions;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;
using savings_app_backend.Repositories.Interfaces;
using savings_app_backend.SavingToFile;
using savings_app_backend.Services.Interfaces;
using System.Security.Claims;

namespace savings_app_backend.Services.Implementations
{
    public class BuyerService : IBuyerService
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private readonly IFileSaver _fileSaver;

        public BuyerService(IHttpContextAccessor httpContext,
            IWebHostEnvironment webHostEnvironment, IConfiguration config, IBuyerRepository buyerRepository, IFileSaver fileSaver)
        {
            _buyerRepository = buyerRepository;
            _httpContext = httpContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
            _fileSaver = fileSaver;
        }

        public async Task<Buyer> DeleteBuyer(Guid id)
        {
            if (id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();

           
            var buyer = await _buyerRepository.GetBuyer(id);
            if (buyer == null)
            {
                throw new RecourseNotFoundException();
            }

            await _buyerRepository.RemoveBuyer(buyer);
            return buyer;
        }

        public async Task<Buyer> GetBuyer(Guid id)
        {
            var buyer = await _buyerRepository.GetBuyer(id);

            if(buyer == null)
            {
                throw new RecourseNotFoundException();
            }
            else
            {
                return buyer;
            }
        }

        public async Task<IEnumerable<Buyer>> GetBuyers()
        {
            return await _buyerRepository.GetBuyers();
        }

        public async Task<Buyer> PostBuyer(Buyer buyer)
        {
            buyer.Id = Guid.NewGuid();

            var imageName = buyer.Id.ToString();

            Task saveImageTask = _fileSaver.SaveImage(buyer.ImageFile, imageName,
                    _config["ImageStorage:ImageFoldersPaths:UserImages"],
                    _config["ImageStorage:ImageFoldersPaths:ImageExtention"]);

            buyer.ImageName = imageName;

            

            await saveImageTask;
            await _buyerRepository.AddBuyer(buyer);

            return buyer;
        }

        public async Task<Buyer> PutBuyer(Guid id, Buyer buyer)
        {
            if (id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();

            if (id != buyer.Id)
            {
                throw new InvalidRequestArgumentsException();
            }


            if (!await _buyerRepository.BuyerExists(id))
            {
                throw new RecourseAlreadyExistsException();
            }

            await _buyerRepository.UpdateBuyer(buyer);
            return buyer;
        }
    }
}
