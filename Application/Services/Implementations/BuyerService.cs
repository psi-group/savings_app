using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.Entities;
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

           
            var buyer = await _buyerRepository.GetBuyerAsync(id);
            if (buyer == null)
            {
                throw new RecourseNotFoundException();
            }

            _buyerRepository.RemoveBuyer(buyer);
            await _buyerRepository.SaveChangesAsync();
            return buyer;
        }

        public async Task<Buyer> GetBuyer(Guid id)
        {
            var buyer = await _buyerRepository.GetBuyerAsync(id);

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
            return await _buyerRepository.GetBuyersAsync();
        }

        public async Task<Buyer> PostBuyer(BuyerDTORequest buyerToPost)
        {
            var id = Guid.NewGuid();

            var buyer = new Buyer(
                id,
                buyerToPost.Name,
                new UserAuth(
                    buyerToPost.UserAuth.Password,
                    buyerToPost.UserAuth.Email),
                buyerToPost.Address == null ?
                    null :
                    new Address(
                        buyerToPost.Address.Country,
                        buyerToPost.Address.City,
                        buyerToPost.Address.StreetName,
                        buyerToPost.Address.HouseNumber,
                        buyerToPost.Address.AppartmentNumber,
                        buyerToPost.Address.PostalCode),
                id.ToString());

            //buyer.GenerateId();


            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                Task saveImageTask = _fileSaver.SaveImage(buyerToPost.Image, buyer.ImageName,
                    _webHostEnvironment.ContentRootPath + _config["ImageStorage:ImageFoldersPaths:UserImages"],
                    _config["ImageStorage:ImageExtention"]);


                await saveImageTask;
                await _buyerRepository.AddBuyerAsync(buyer);
                await _buyerRepository.SaveChangesAsync();

                scope.Complete();
            }
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


            if (!await _buyerRepository.BuyerExistsAsync(id))
            {
                throw new RecourseAlreadyExistsException();
            }

            _buyerRepository.UpdateBuyer(buyer);
            await _buyerRepository.SaveChangesAsync();
            return buyer;
        }
    }
}
