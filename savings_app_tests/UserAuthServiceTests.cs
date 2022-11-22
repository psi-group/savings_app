using NSubstitute;
using savings_app_backend.Exceptions;
using savings_app_backend.Models.Entities;
using savings_app_backend.Models.Enums;
using savings_app_backend.Repositories.Implementations;
using savings_app_backend.Repositories.Interfaces;
using savings_app_backend.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace savings_app_tests
{
    public class UserAuthServiceTests
    {
        UserAuthService _sut;
        private readonly IUserAuthRepository _userAuthRepository = Substitute.For<IUserAuthRepository>();

        public UserAuthServiceTests()
        {
            _sut = new UserAuthService(_userAuthRepository);
        }

        [Fact]
        public async Task GetUserAuth_ShouldReturnUserAuth_WhenUserAuthExists()
        {
            //Arrange

            var id = Guid.NewGuid();

            var returnedUserAuth = new UserAuth(id, "password", "email@gmail.com");

            _userAuthRepository.GetUserAuth(id).Returns(returnedUserAuth);

            //Act

            var userAuth = await _sut.GetUserAuth(id);

            //Assert

            Assert.Equal(returnedUserAuth, userAuth);
        }

        [Fact]
        public async Task GetUserAuth_ShouldThrowRecourseNotFoundException_WhenUserAuthDoesNotExist()
        {
            //Arrange

            var id = Guid.NewGuid();

            _userAuthRepository.GetUserAuth(id).Returns(default(UserAuth));

            //Act

            //Assert

            await Assert.ThrowsAsync<RecourseNotFoundException>(async () => await _sut.GetUserAuth(id));
        }
    }
}
