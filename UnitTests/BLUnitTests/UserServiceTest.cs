using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mbti_web.Models;
using mbti_web.Repository;
using mbti_web.Entities;
using mbti_web.Services;
using Moq;
using System;
using UnitTests.DataBuilders;
using mbti_web;
using Allure.Xunit.Attributes;
using UnitTests.ObjectMother;
using Microsoft.Extensions.Configuration;
using mbti_web.Middleware;
using UnitTests.Helpers;


namespace UnitTests
{
    [AllureSuite("UserServiceSuite")]
    public class UserServiceTest
    {
        //[AllureXunit]
        public void TestGetById()
        {
            // Arrange
            User expectedUser = new UserObjectMother().ExistingUserLena();

            var userRepStub = new Mock<IRepositoryUser>();
            {
                userRepStub.Setup(x => x.Find(expectedUser.Useruk)).Returns(expectedUser);
            }

            UserService userService = new UserService(userRepStub.Object, null, null);

            // Act
            UserModel actualUser = userService.GetById(expectedUser.Useruk);

            // Assert
            /*Assert.Equal(expectedUser.Useruk, actualUser.ID);
            Assert.Equal(expectedUser.Login, actualUser.Login);
            Assert.Equal(expectedUser.Password, actualUser.Password);
            Assert.Equal(expectedUser.Nickname, actualUser.Nickname);
            Assert.Equal(expectedUser.Email, actualUser.Email);
            Assert.Equal(expectedUser.Telagram, actualUser.Telagram);
            Assert.Equal(expectedUser.Dateofbirth, actualUser.Dateofbirth);*/
            Assert.Null(expectedUser);
        }

        //[AllureXunit]
        public void TestGetAll()
        {
            // Arrange
            List<User> expectedUsers = new List<User>
            {
                new UserObjectMother().ExistingUserLena(),
                new UserObjectMother().ExistingUserMasha(),
                new UserObjectMother().TestUser()
            };

            var userRepStub = new Mock<IRepositoryUser>();
            {
                userRepStub.Setup(x => x.GetAll()).Returns(expectedUsers);
            }

            UserService userService = new UserService(userRepStub.Object, null, null);

            // Act
            List<UserModel> userList = userService.GetAll().ToList();

            // Assert
            Assert.Null(userList);
        }

        [AllureXunitTheory]
        [InlineData("lena")]
        public void TestCheckLoginUnique(string _login)
        {
            // Arrange
            List<User> expectedUsers = new List<User>
            {
                new UserObjectMother().ExistingUserLena(),
                new UserObjectMother().ExistingUserMasha(),
                new UserObjectMother().TestUser()
            };

            var userRepStub = new Mock<IRepositoryUser>();
            {
                userRepStub.Setup(x => x.GetAll()).Returns(expectedUsers);
            }

            UserService userService = new UserService(userRepStub.Object, null, null);

            // Act
            bool res = userService.CheckLoginUnique(_login);

            // Assert
            Assert.False(res);
        }

        [AllureXunit]
        public void TestAuthenticate()
        {
            // Arrange
            User user = new UserObjectMother().ExistingUserLena();
            List<User> expectedUsers = new List<User> { user };

            var userRepStub = new Mock<IRepositoryUser>();
            {
                userRepStub.Setup(x => x.GetAll()).Returns(expectedUsers);
            }

            AuthenticateRequest authUserReq = new AuthenticateRequest();
            authUserReq.Login = "lena";
            authUserReq.Password = "123";

            var inMemorySettings = new Dictionary<string, string>
            {
                {"Secret", "0123456789123456"}
            };

            IConfiguration configurationInMemory = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            UserService userService = new UserService(userRepStub.Object, configurationInMemory, null);

            //Act
            AuthenticateResponse authUserResp = userService.Authenticate(authUserReq);

            // Assert
            Assert.NotNull(authUserResp.Token);
        }
    }
}