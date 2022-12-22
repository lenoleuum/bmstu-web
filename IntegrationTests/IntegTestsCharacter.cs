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
    [AllureSuite("IntegrationTestCharacter")]
    public class IntegTestsCharacter
    {
        private readonly CharacterService _characterService;
        public IntegTestsCharacter()
        {
            var _server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            _characterService = (CharacterService)_server.Services.GetRequiredService<ICharacterService>();
        }

        [AllureXunit]
        public void TestGetAll()
        {
            // Arrange

            // Act
            List<CharacterModel> characters = _characterService.GetAllCharacters();

            // Assert
            Assert.NotNull(characters);
        }

        [AllureXunitTheory]
        [InlineData("ISTJ")]
        public void TestGetCharacterByType(string _type)
        {
            // Arrange

            // Act
            List<CharacterModel> characters = _characterService.GetCharacterByType(_type);

            // Assert
            Assert.NotNull(characters);
            Assert.Equal(_type, characters[0].Type);
        }

        [AllureXunitTheory]
        [InlineData("IS")]
        public void TestGetCharacterByStrLikeType(string _str, int _flag = 1)
        {
            // Arrange

            // Act
            List<CharacterModel> characters = _characterService.GetCharacterByStrLike(_str, _flag);

            // Assert
            Assert.NotNull(characters);
            Assert.Contains(_str, characters[0].Type);
        }

        [AllureXunitTheory]
        [InlineData("AB")]
        public void TestGetCharacterByStrLikeTypeNull(string _str, int _flag = 1)
        {
            // Arrange

            // Act
            List<CharacterModel> characters = _characterService.GetCharacterByStrLike(_str, _flag);

            // Assert
            Assert.Empty(characters);
        }

        [AllureXunitTheory]
        [InlineData("Aki")]
        public void TestGetCharacterByStrLikeName(string _str, int _flag = 3)
        {
            // Arrange

            // Act
            List<CharacterModel> characters = _characterService.GetCharacterByStrLike(_str, _flag);

            // Assert
            Assert.NotNull(characters);
            Assert.Contains(_str.ToLower(), characters[0].Name.ToLower());
        }

        [AllureXunitTheory]
        [InlineData("Music")]
        public void TestGetCharacterByStrLikeCategory(string _str, int _flag = 2)
        {
            // Arrange

            // Act
            List<CharacterModel> characters = _characterService.GetCharacterByStrLike(_str, _flag);

            // Assert
            Assert.NotNull(characters);
            Assert.Contains(_str, characters[0].Category);
        }
    }
}
