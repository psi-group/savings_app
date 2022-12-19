using Application.Services.Implementations;
using Domain.DTOs.Request;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Linq.Expressions;
using System.Security.Claims;
using Xunit;

namespace savings_app_tests
{
    public class BuyerServiceTests
    {
        private readonly BuyerService _sut;

        private readonly IBuyerRepository _buyerRepository = Substitute.For<IBuyerRepository>();

        private readonly IFileSaver _fileSaver = Substitute.For<IFileSaver>();
        private readonly IHttpContextAccessor _httpContext = Substitute.For<IHttpContextAccessor>();

        public BuyerServiceTests()
        {
            _sut = new BuyerService(_httpContext, _buyerRepository, _fileSaver);
        }

        [Fact]
        public async Task GetBuyers_ShouldReturnListOfBuyers()
        {
            //Arrange

            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            _buyerRepository.GetBuyersAsync().Returns(new List<Buyer>()
            {
                new Buyer(
                    id1,
                    "name1",
                    new UserAuth("password1", "email1"),
                    null,
                    null
                    ),
                new Buyer(
                    id2,
                    "name2",
                    new UserAuth("password2", "email2"),
                    null,
                    null
                    )
            });

            //Act

            var returnedBuyers = (List<BuyerDTOResponse>)await _sut.GetBuyers();
            //Assert


            Assert.Equal(2, returnedBuyers.Count());

            Assert.Equal(id1, returnedBuyers[0].Id);
            Assert.Equal("name1", returnedBuyers[0].Name);
            Assert.Null(returnedBuyers[0].ImageUrl);

            Assert.Equal(id2, returnedBuyers[1].Id);
            Assert.Equal("name2", returnedBuyers[1].Name);
            Assert.Null(returnedBuyers[1].ImageUrl);
        }

        [Fact]
        public async Task GetBuyer_ShouldReturnBuyer_WhenBuyerExists()
        {
            //Arrange

            var id = Guid.NewGuid();

            var buyer = new Buyer(
                id,
                "user",
                new UserAuth("password", "email"),
                new Address("country", "city", "streetName", 0, 0, 0),
                "imageUrl"
                );

            var buyerDTO = new BuyerDTOResponse(
                id,
                "user",
                "imageUrl"
                );

            _buyerRepository.GetBuyerAsync(buyer.Id).Returns(buyer);

            //Act

            var returnedBuyer = await _sut.GetBuyer(buyer.Id);

            var returnedBuyerDTO = new BuyerDTOResponse(returnedBuyer.Id, returnedBuyer.Name, returnedBuyer.ImageUrl);

            //Assert


            Assert.Equal(buyerDTO.Id, returnedBuyerDTO.Id);
            Assert.Equal(buyerDTO.Name, returnedBuyerDTO.Name);
            Assert.Equal(buyerDTO.ImageUrl, returnedBuyerDTO.ImageUrl);
        }

        [Fact]
        public async Task GetBuyer_ShouldThrowRecourseNotFoundException_WhenBuyerDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            _buyerRepository.GetBuyerAsync(id).Returns(default(Buyer));

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetBuyer(id));
        }

        [Fact]
        public async Task GetBuyerPrivate_ShouldReturnBuyerWithNullAddress_WhenBuyerExistsAndIdentityMatchesGivenIdAndBuyerInDatabaseHasNullAddress()
        {
            //Arrange

            var id = Guid.NewGuid();

            var buyer = new Buyer(
                id,
                "user",
                new UserAuth("password", "email"),
                null,
                "imageUrl"
                );

            var buyerDTO = new BuyerPrivateDTOResponse(
                id,
                "user",
                "imageUrl",
                null,
                new UserAuthDTOResponse(
                    "email"
                    )
                );

            _buyerRepository.GetBuyerAsync(buyer.Id).Returns(buyer);


            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", id.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act

            var returnedBuyer = await _sut.GetBuyerPrivate(buyer.Id);

            

            //Assert


            Assert.Equal(buyerDTO.Id, returnedBuyer.Id);
            Assert.Equal(buyerDTO.Name, returnedBuyer.Name);
            Assert.Equal(buyerDTO.ImageUrl, returnedBuyer.ImageUrl);
            Assert.Equal(buyerDTO.Address, returnedBuyer.Address);
            Assert.Equal(buyerDTO.UserAuth.Email, returnedBuyer.UserAuth.Email);
        }

        [Fact]
        public async Task GetBuyerPrivate_ShouldReturnBuyerWithAddress_WhenBuyerExistsAndIdentityMatchesGivenIdAndBuyerInDatabaseHasAddress()
        {
            //Arrange

            var id = Guid.NewGuid();

            var buyer = new Buyer(
                id,
                "user",
                new UserAuth("password", "email"),
                new Address(
                    "country",
                    "city",
                    "street",
                    1,
                    1,
                    1
                    ),
                "imageUrl"
                );

            var buyerDTO = new BuyerPrivateDTOResponse(
                id,
                "user",
                "imageUrl",
                new AddressDTOResponse(
                    "country",
                    "city",
                    "street",
                    1,
                    1,
                    1
                    ),
                new UserAuthDTOResponse(
                    "email"
                    )
                );

            _buyerRepository.GetBuyerAsync(buyer.Id).Returns(buyer);


            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", id.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act

            var returnedBuyer = await _sut.GetBuyerPrivate(buyer.Id);



            //Assert


            Assert.Equal(buyerDTO.Id, returnedBuyer.Id);
            Assert.Equal(buyerDTO.Name, returnedBuyer.Name);
            Assert.Equal(buyerDTO.ImageUrl, returnedBuyer.ImageUrl);
            Assert.Equal(buyerDTO.Address!.Country, returnedBuyer.Address!.Country);
            Assert.Equal(buyerDTO.Address!.City, returnedBuyer.Address!.City);
            Assert.Equal(buyerDTO.Address!.StreetName, returnedBuyer.Address!.StreetName);
            Assert.Equal(buyerDTO.Address!.HouseNumber, returnedBuyer.Address!.HouseNumber);
            Assert.Equal(buyerDTO.Address!.AppartmentNumber, returnedBuyer.Address!.AppartmentNumber);
            Assert.Equal(buyerDTO.Address!.PostalCode, returnedBuyer.Address!.PostalCode);


            Assert.Equal(buyerDTO.UserAuth.Email, returnedBuyer.UserAuth.Email);
        }

        [Fact]
        public async Task GetBuyerPrivate_ShouldThrowRecourseNotFoundException_WhenBuyerDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", id.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            _buyerRepository.GetBuyerAsync(id).Returns((Buyer?)null);

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetBuyerPrivate(id));

        }

        [Fact]
        public async Task PostBuyer_ShouldThrowRecourseAlreadyExistsException_WhenBuyerWithEmailAlreadyExists()
        {
            //Arrange

            var buyer = new Buyer(
                Guid.NewGuid(),
                "user",
                new UserAuth("password", "email"),
                new Address("country", "city", "streetName", 0, 0, 0),
                "imageUrl"
                );

            var buyerToPost = new BuyerDTORequest(
                "buyer",
                new UserAuthDTORequest(
                    "email",
                    "password"
                    ),
                null,
                null
                );


            _buyerRepository.GetBuyerAsync(Arg.Any<Expression<Func<Buyer, bool>>>()).Returns(buyer);
         
            //Act

            //Assert

            var exc = await Assert.ThrowsAsync<RecourseAlreadyExistsException>(async () => await _sut.PostBuyer(buyerToPost));
            Assert.Equal("buyer with this email already exists", exc.Message);
        }

        [Fact]
        public async Task PostBuyer_ShouldReturnBuyerDTOResponseWithDefaultImageUrlAndShouldNotSaveToFileAndAddToDatabase_WhenAtLeastNameAndUserAuthAreNotNullAndImageIsNull()
        {
            //Arrange

            var buyerToPost = new BuyerDTORequest(
                "buyer",
                new UserAuthDTORequest(
                    "email",
                    "password"
                    ),
                null,
                null
                );

            var buyDTOResponse = new BuyerDTOResponse(
                Guid.NewGuid(),
                buyerToPost.Name!,
                null
                );


            int saveImageCounter = 0;
            int saveChangesAsyncCounter = 0;
            int addBuyerAsyncCounter = 0;

            _fileSaver.When(async x => await x.SaveImage(Arg.Any<IFormFile>(), Arg.Any<String>(), Arg.Any<bool>()))
                .Do(x => saveImageCounter++);

            _buyerRepository.When(async x => await x.SaveChangesAsync())
                .Do(x => saveChangesAsyncCounter++);

            _buyerRepository.When(async x => await x.AddBuyerAsync(Arg.Any<Buyer>()))
                .Do(x => addBuyerAsyncCounter++);

            _buyerRepository.GetBuyerAsync(Arg.Any<Expression<Func<Buyer, bool>>>()).Returns((Buyer?)null);

            //Act

            var returnedBuyerDTOResponse = await _sut.PostBuyer(buyerToPost);

            //Assert

            Assert.Equal(buyDTOResponse.Name, returnedBuyerDTOResponse.Name);
            Assert.Equal(0, saveImageCounter);
            Assert.Equal(1, saveChangesAsyncCounter);
            Assert.Equal(1, addBuyerAsyncCounter);

        }


        [Fact]
        public async Task PostBuyer_ShouldReturnBuyerDTOResponseAndSaveToFileAndAddToDatabase_WhenAtLeastNameAndUserAuthAreNotNullAndImageIsNotNull()
        {
            //Arrange

            var buyerToPost = new BuyerDTORequest(
                "buyer",
                new UserAuthDTORequest(
                    "email",
                    "password"
                    ),
                new AddressDTORequest(
                    "country",
                    "city",
                    "streetName",
                    1,
                    1,
                    1
                    ),
                Substitute.For<IFormFile>()
                );

            var buyDTOResponse = new BuyerDTOResponse(
                Guid.NewGuid(),
                buyerToPost.Name!,
                null
                );


            int saveImageCounter = 0;
            int saveChangesAsyncCounter = 0;
            int addBuyerAsyncCounter = 0;

            _fileSaver.When(async x => await x.SaveImage(Arg.Any<IFormFile>(), Arg.Any<String>(), Arg.Any<bool>()))
                .Do(x => saveImageCounter++);

            _buyerRepository.When(async x => await x.SaveChangesAsync())
                .Do(x => saveChangesAsyncCounter++);

            _buyerRepository.When(async x => await x.AddBuyerAsync(Arg.Any<Buyer>()))
                .Do(x => addBuyerAsyncCounter++);

            _buyerRepository.GetBuyerAsync(Arg.Any<Expression<Func<Buyer, bool>>>()).Returns((Buyer?)null);

            //Act

            var returnedBuyerDTOResponse = await _sut.PostBuyer(buyerToPost);

            //Assert

            Assert.Equal(buyDTOResponse.Name, returnedBuyerDTOResponse.Name);
            Assert.Equal(1, saveImageCounter);
            Assert.Equal(1, saveChangesAsyncCounter);
            Assert.Equal(1, addBuyerAsyncCounter);

        }

        [Fact]
        public async Task PutBuyer_ShouldThrowInvalidIdentityException_WhenBuyerTriesToChangeNotHisAccount()
        {
            //Arrange

            var idToChange = Guid.NewGuid();

            var buyerToPut = new BuyerDTORequest(
                "buyer",
                new UserAuthDTORequest(
                    "email",
                    "password"
                    ),
                null,
                null
                );


            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", default(Guid).ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act

            //Assert

            var exc = await Assert.ThrowsAsync<InvalidIdentityException>(async () => await _sut.PutBuyer(idToChange, buyerToPut));
            Assert.Equal("you are unauthorized to update this resource", exc.Message);

        }

        [Fact]
        public async Task PutBuyer_ShouldThrowRecourseNotFoundException_WhenBuyerWithPassedIdDoesntExist()
        {
            //Arrange

            var idToChange = Guid.NewGuid();

            var buyerToPut = new BuyerDTORequest(
                "buyer",
                new UserAuthDTORequest(
                    "email",
                    "password"
                    ),
                null,
                null
                );

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", idToChange.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            _buyerRepository.BuyerExistsAsync(idToChange).Returns(false);

            //Act

            //Assert

            var exc = await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.PutBuyer(idToChange, buyerToPut));
            Assert.Equal("buyer with this id does not exist", exc.Message);

        }

        [Fact]
        public async Task PutBuyer_ShouldThrowRecourseAlreadyExistsException_WhenBuyerWithPassedEmailAlreadyExists()
        {
            //Arrange

            var idToChange = Guid.NewGuid();

            var buyerToPut = new BuyerDTORequest(
                "buyer",
                new UserAuthDTORequest(
                    "email",
                    "password"
                    ),
                null,
                null
                );

            var buyer = new Buyer(
                idToChange,
                "name",
                new UserAuth(
                    "password",
                    "email"
                    ),
                null,
                null
                );

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", idToChange.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            _buyerRepository.BuyerExistsAsync(idToChange).Returns(true);
            _buyerRepository.GetBuyerAsync(Arg.Any<Expression<Func<Buyer, bool>>>()).Returns(buyer);

            //Act

            //Assert

            var exc = await Assert.ThrowsAsync<RecourseAlreadyExistsException>(async () => await _sut.PutBuyer(idToChange, buyerToPut));
            Assert.Equal("buyer with this email already exists", exc.Message);

        }

        [Fact]
        public async Task PutBuyer_ShouldReturnBuyerDTOResponseAndSaveToFileAndUpdateDatabase_WhenAtLeastNameAndUserAuthAreNotNullAndImageIsNotNull()
        {
            //Arrange

            var idToChange = Guid.NewGuid();

            var buyerToPut = new BuyerDTORequest(
                "buyer",
                new UserAuthDTORequest(
                    "email",
                    "password"
                    ),
                new AddressDTORequest(
                    "country",
                    "city",
                    "streetName",
                    1,
                    1,
                    1
                    ),
                Substitute.For<IFormFile>()
                );

            var buyDTOResponse = new BuyerDTOResponse(
                idToChange,
                buyerToPut.Name!,
                "https://savingsapp.blob.core.windows.net/userimages/" + idToChange + ".jpg"
                );

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", idToChange.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);


            int saveImageCounter = 0;
            int saveChangesAsyncCounter = 0;
            int updateBuyerCounter = 0;

            _fileSaver.When(async x => await x.SaveImage(Arg.Any<IFormFile>(), Arg.Any<String>(), Arg.Any<bool>()))
                .Do(x => saveImageCounter++);

            _buyerRepository.When(async x => await x.SaveChangesAsync())
                .Do(x => saveChangesAsyncCounter++);

            _buyerRepository.When(x => x.UpdateBuyer(Arg.Any<Buyer>()))
                .Do(x => updateBuyerCounter++);



            _buyerRepository.GetBuyerAsync(Arg.Any<Expression<Func<Buyer, bool>>>()).Returns((Buyer?)null);

            _buyerRepository.BuyerExistsAsync(idToChange).Returns(true);

            //Act

            var returnedBuyerDTOResponse = await _sut.PutBuyer(idToChange, buyerToPut);

            //Assert

            Assert.Equal(buyDTOResponse.Name, returnedBuyerDTOResponse.Name);
            Assert.Equal(buyDTOResponse.Id, returnedBuyerDTOResponse.Id);
            Assert.Equal(buyDTOResponse.ImageUrl, returnedBuyerDTOResponse.ImageUrl);

            Assert.Equal(1, saveImageCounter);
            Assert.Equal(1, saveChangesAsyncCounter);
            Assert.Equal(1, updateBuyerCounter);

        }

        [Fact]
        public async Task PutBuyer_ShouldReturnBuyerDTOResponseWithNullAddressAndSaveToFileAndUpdateDatabase_WhenAtLeastNameAndUserAuthAreNotNullAndImageIsNotNullAndAdressIsNull()
        {
            //Arrange

            var idToChange = Guid.NewGuid();

            var buyerToPut = new BuyerDTORequest(
                "buyer",
                new UserAuthDTORequest(
                    "email",
                    "password"
                    ),
                null,
                Substitute.For<IFormFile>()
                );

            var buyDTOResponse = new BuyerDTOResponse(
                idToChange,
                buyerToPut.Name!,
                "https://savingsapp.blob.core.windows.net/userimages/" + idToChange + ".jpg"
                );

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", idToChange.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);


            int saveImageCounter = 0;
            int saveChangesAsyncCounter = 0;
            int updateBuyerCounter = 0;

            _fileSaver.When(async x => await x.SaveImage(Arg.Any<IFormFile>(), Arg.Any<String>(), Arg.Any<bool>()))
                .Do(x => saveImageCounter++);

            _buyerRepository.When(async x => await x.SaveChangesAsync())
                .Do(x => saveChangesAsyncCounter++);

            _buyerRepository.When(x => x.UpdateBuyer(Arg.Any<Buyer>()))
                .Do(x => updateBuyerCounter++);



            _buyerRepository.GetBuyerAsync(Arg.Any<Expression<Func<Buyer, bool>>>()).Returns((Buyer?)null);

            _buyerRepository.BuyerExistsAsync(idToChange).Returns(true);

            //Act

            var returnedBuyerDTOResponse = await _sut.PutBuyer(idToChange, buyerToPut);

            //Assert

            Assert.Equal(buyDTOResponse.Name, returnedBuyerDTOResponse.Name);
            Assert.Equal(buyDTOResponse.Id, returnedBuyerDTOResponse.Id);
            Assert.Equal(buyDTOResponse.ImageUrl, returnedBuyerDTOResponse.ImageUrl);

            Assert.Equal(1, saveImageCounter);
            Assert.Equal(1, saveChangesAsyncCounter);
            Assert.Equal(1, updateBuyerCounter);

        }

        [Fact]
        public async Task DeleteBuyer_ShouldThrowInvalidIdentityException_WhenBuyerTriesToDeleteNotHisAccount()
        {
            //Arrange

            var idToDelete = Guid.NewGuid();


            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", default(Guid).ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            //Act

            //Assert

            var exc = await Assert.ThrowsAsync<InvalidIdentityException>(async () => await _sut.DeleteBuyer(idToDelete));
            Assert.Equal("you are unauthorized to delete this recource", exc.Message);

        }


        [Fact]
        public async Task DeleteBuyer_ShouldThrowRecourseNotFoundException_WhenBuyerWithPassedIdDoesntExist()
        {
            //Arrange

            var idToDelete = Guid.NewGuid();

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", idToDelete.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            _buyerRepository.GetBuyerAsync(idToDelete).Returns((Buyer?)null);

            //Act

            //Assert

            var exc = await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.DeleteBuyer(idToDelete));
            Assert.Equal("buyer with this id does not exist", exc.Message);

        }

        [Fact]
        public async Task DeleteBuyer_ShouldReturnDeletedBuyerDTOResponse_WhenBuyerWithPassedIdExistsAndUserIndentityMatchesGivenId()
        {
            //Arrange

            var idToDelete = Guid.NewGuid();

            var claimsIdentity = new ClaimsIdentity(new List<Claim>() { new Claim("Id", idToDelete.ToString()) });

            HttpContext httpctx = Substitute.For<HttpContext>();

            _httpContext.HttpContext = httpctx;

            _httpContext.HttpContext.User.Identity.Returns(claimsIdentity);

            var deleteBuyerDTOResponse = new BuyerDTOResponse(
                idToDelete,
                "name",
                null
                );

            var buyer = new Buyer(
                idToDelete,
                "name",
                new UserAuth(
                    "password",
                    "email"
                    ),
                null,
                null
                );

            int saveChangesAsyncCounter = 0;
            int removeBuyerCounter = 0;


            _buyerRepository.When(async x => await x.SaveChangesAsync())
                .Do(x => saveChangesAsyncCounter++);

            _buyerRepository.When(x => x.RemoveBuyer(Arg.Any<Buyer>()))
                .Do(x => removeBuyerCounter++);

            _buyerRepository.GetBuyerAsync(idToDelete).Returns(buyer);

            //Act

            var returnedBuyerDTOResponse = await _sut.DeleteBuyer(idToDelete);

            //Assert

            Assert.Equal(deleteBuyerDTOResponse.Name, returnedBuyerDTOResponse.Name);
            Assert.Equal(deleteBuyerDTOResponse.Id, returnedBuyerDTOResponse.Id);
            Assert.Equal(deleteBuyerDTOResponse.ImageUrl, returnedBuyerDTOResponse.ImageUrl);

            Assert.Equal(1, saveChangesAsyncCounter);
            Assert.Equal(1, removeBuyerCounter);

        }
    }
}
