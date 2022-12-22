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
    [AllureSuite("IntegrationTestType")]
    public class IntegTestsType
    {
        private readonly TypeService _typeService;
        public IntegTestsType()
        {
            var _server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            _typeService = (TypeService)_server.Services.GetRequiredService<ITypeService>();
        }

        [AllureXunit]
        public void TestGetAll()
        {
            // Arrange
            int cntTypes = 16;

            // Act
            List<TypeModel> types = _typeService.GetAllTypes();

            // Assert
            Assert.NotNull(types);
            Assert.Equal(cntTypes, types.Count);
        }

        [AllureXunitTheory]
        [InlineData("ENTP")]
        public void TestGetByName(string _str)
        {
            // Arrange

            // Act
            List<TypeModel> types = _typeService.GetTypeByNameLike(_str);

            // Assert
            Assert.NotNull(types);
            Assert.Contains(_str, types[0].Name);
        }

        [AllureXunitTheory]
        [InlineData(8)]
        public void TestGetById(int _id)
        {
            // Arrange

            // Act
            TypeModel type = _typeService.GetTypeByID(_id);

            // Assert
            Assert.NotNull(type);
            Assert.Equal(_id, type.ID);
            Assert.Equal(new TypesDict().getTypeById(_id), type.Name);
        }
    }
}
