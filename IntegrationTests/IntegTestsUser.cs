using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mbti_web.Models;
using mbti_web.Repository;
using mbti_web.Entities;
using mbti_web.Controllers;
using mbti_web.Services;
using mbti_web.Mappers;
using System;
using mbti_web;
using Moq;
using Allure.Xunit.Attributes;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using Microsoft.AspNetCore.Hosting;
using IntegrationTests.Helpers;

namespace IntegrationTests
{
    [AllureSuite("IntegrationTestUser")]
    public class IntegTestsUser
    {
        private IUserService _userService;

        public IntegTestsUser()
        {
            var _server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            _userService = (UserService)_server.Services.GetRequiredService<IUserService>();
        }

        [AllureXunit]
        public void TestGetAll()
        {
            // Arrange

            // Act
            List<UserModel> users = _userService.GetAll();

            // Assert
            Assert.NotNull(users);
        }

        [AllureXunitTheory]
        [InlineData("lena", "123")]
        public void TestAuthenticate(string _login, string _password)
        {
            // Arrange
            AuthenticateRequest authRequest = new AuthenticateRequest(_login, _password);

            // Act
            AuthenticateResponse response = _userService.Authenticate(authRequest);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Token);
            Assert.Equal(authRequest.Login, response.Login);
        }

        [AllureXunitTheory]
        [AutoMoqData]
        public void TestAuthenticateBadRequest(string _login, string _password)
        {
            // Arrange
            AuthenticateRequest authRequest = new AuthenticateRequest(_login, _password);

            // Act
            AuthenticateResponse response = _userService.Authenticate(authRequest);

            // Assert
            Assert.Null(response);
        }

        [AllureXunitTheory]
        [AutoMoqData]
        public void TestRegister(string _login, string _password)
        {
            // Arrange
            AuthenticateRequest regRequest = new AuthenticateRequest(_login, _password);

            // Act
            _userService.Register(regRequest);

            // Assert
        }

        [AllureXunitTheory]
        [InlineData(1)]
        public void TestGetUserById(int _id)
        {
            // Arrange

            // Act
            UserModel user = _userService.GetById(_id);

            // Assert
            Assert.NotNull(user);
        }

        public class AutoMoqDataAttribute : AutoDataAttribute
        {
            public AutoMoqDataAttribute()
#pragma warning disable CS0618 // Type or member is obsolete
            : base(new AutoFixture.Fixture().Customize(new AutoMoqCustomization()))
#pragma warning restore CS0618 // Type or member is obsolete
            { }
        }
    }
}