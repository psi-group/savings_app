using Application.Services.Interfaces;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Transactions;

namespace Application.Services.Implementations
{
    public class BuyerService : IBuyerService
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IFileSaver _fileSaver;

        public BuyerService(IHttpContextAccessor httpContext,
            IBuyerRepository buyerRepository, IFileSaver fileSaver)
        {
            _buyerRepository = buyerRepository;
            _httpContext = httpContext;
            _fileSaver = fileSaver;
        }

        public async Task<BuyerDTOResponse> DeleteBuyer(Guid id)
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

            var buyerResponse = new BuyerDTOResponse(
                buyer.Id,
                buyer.Name,
                buyer.ImageName
                );

            return buyerResponse;
        }


        public async Task<BuyerPrivateDTOResponse> GetBuyerPrivate(Guid id)
        {

            if (id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();

            var buyer = await _buyerRepository.GetBuyerAsync(id);

            if (buyer == null)
            {
                throw new RecourseNotFoundException();
            }
            else
            {
                var buyerResponse = new BuyerPrivateDTOResponse(
                buyer.Id,
                buyer.Name,
                buyer.ImageName,
                buyer.Address == null ? null :
                new AddressDTOResponse(
                    buyer.Address.Country,
                    buyer.Address.City,
                    buyer.Address.StreetName,
                    buyer.Address.HouseNumber,
                    buyer.Address.AppartmentNumber,
                    buyer.Address.PostalCode
                    ),
                new UserAuthDTOResponse(
                    buyer.UserAuth.Email
                    )
                );
                return buyerResponse;
            }
        }

        public async Task<BuyerDTOResponse> GetBuyer(Guid id)
        {
            var buyer = await _buyerRepository.GetBuyerAsync(id);

            if(buyer == null)
            {
                throw new RecourseNotFoundException();
            }
            else
            {
                var buyerResponse = new BuyerDTOResponse(
                buyer.Id,
                buyer.Name,
                buyer.ImageName
                );
                return buyerResponse;
            }
        }

        public async Task<IEnumerable<BuyerDTOResponse>> GetBuyers()
        {
            var buyers =  await _buyerRepository.GetBuyersAsync();
            var buyersResponse = new List<BuyerDTOResponse>();
            foreach(var buyer in buyers)
            {
                var buyerResponse = new BuyerDTOResponse(
                buyer.Id,
                buyer.Name,
                buyer.ImageName
                );
                buyersResponse.Add(buyerResponse);
            }
            return buyersResponse;
        }

        public async Task<BuyerDTOResponse> PostBuyer(BuyerDTORequest buyerToPost)
        {
            if (await _buyerRepository.GetBuyerAsync(
                buyer => buyer.UserAuth.Email == buyerToPost.UserAuth!.Email) != null)
            {
                throw new RecourseAlreadyExistsException();
            }

            var id = Guid.NewGuid();

            var buyer = new Buyer(
                id,
                buyerToPost.Name!,
                new UserAuth(
                    buyerToPost.UserAuth!.Password!,
                    buyerToPost.UserAuth!.Email!),
                buyerToPost.Address == null ?
                    null :
                    new Address(
                        buyerToPost.Address.Country!,
                        buyerToPost.Address.City!,
                        buyerToPost.Address.StreetName!,
                        (int)buyerToPost.Address.HouseNumber!,
                        buyerToPost.Address.AppartmentNumber,
                        (int)buyerToPost.Address.PostalCode!),
                id.ToString());

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _buyerRepository.AddBuyerAsync(buyer);
                await _buyerRepository.SaveChangesAsync();

                if (buyerToPost.Image != null)
                {
                    Task saveImageTask = _fileSaver.SaveImage(buyerToPost.Image,
                        buyer.ImageName, false);
                    await saveImageTask;
                }

                scope.Complete();
            }

            var buyerResponse = new BuyerDTOResponse(
                buyer.Id,
                buyer.Name,
                buyer.ImageName
                ) ;

            return buyerResponse;
        }

        public async Task<BuyerDTOResponse> PutBuyer(Guid id, BuyerDTORequest buyerToUpdate)
        {
            if (id !=
                Guid.Parse(((ClaimsIdentity)_httpContext.HttpContext.User.Identity).FindFirst("Id").Value))
                throw new InvalidIdentityException();


            if (!await _buyerRepository.BuyerExistsAsync(id))
            {
                throw new RecourseAlreadyExistsException();
            }

            var buyer = new Buyer(
                id,
                buyerToUpdate.Name!,
                new UserAuth(
                    buyerToUpdate.UserAuth!.Password!,
                    buyerToUpdate.UserAuth!.Email!),
                buyerToUpdate.Address == null ?
                    null :
                    new Address(
                        buyerToUpdate.Address.Country!,
                        buyerToUpdate.Address.City!,
                        buyerToUpdate.Address.StreetName!,
                        (int)buyerToUpdate.Address.HouseNumber!,
                        buyerToUpdate.Address.AppartmentNumber,
                        (int)buyerToUpdate.Address.PostalCode!),
                id.ToString());

            _buyerRepository.UpdateBuyer(buyer);
            await _buyerRepository.SaveChangesAsync();

            var buyerResponse = new BuyerDTOResponse(
                buyer.Id,
                buyer.Name,
                buyer.ImageName
                );

            return buyerResponse;
        }
    }
}
