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


namespace UnitTests.BLUnitTests
{
    [AllureSuite("CharacterServiceSuite")]
    public class CharacterServiceTest
    {
        //[AllureXunit]
        public void TestGetCharacterById()
        {
            // Arrange
            Character expectedCharacter = new CharacterObjectMother().ESTPCharacterNew();

            var mockRepStub = new Mock<IRepositoryCharacter>();
            {
                mockRepStub.Setup(rep => rep.Find(expectedCharacter.Characteruk)).Returns(expectedCharacter);
            }

            CharacterService characterService = new CharacterService(null, mockRepStub.Object);

            // Act
            CharacterModel actualCharacter = characterService.GetCharacterByID(expectedCharacter.Characteruk);

            // Assert
            Assert.Equal(expectedCharacter.Characteruk, actualCharacter.ID);
            Assert.Equal(expectedCharacter.Charactername, actualCharacter.Name);
            Assert.Equal(expectedCharacter.Category, actualCharacter.Category);
        }

        //[AllureXunit]
        public void TestGetAllCharacters()
        {
            // Arrange
            List<Character> characters = new List<Character>
            {
                new CharacterObjectMother().ESTPCharacterNew(),
                new CharacterObjectMother().ISTJCharacterNew(),
                new CharacterObjectMother().ENTPCharacterNew()
            };

            var mockRepStub = new Mock<IRepositoryCharacter>();
            {
                mockRepStub.Setup(rep => rep.GetAll()).Returns(characters);
            }

            CharacterService characterService = new CharacterService(null, mockRepStub.Object);

            // Act
            List<CharacterModel> charactersList = characterService.GetAllCharacters().ToList();

            // Assert
            Assert.NotNull(charactersList);
        }

        //[AllureXunitTheory]
        //[InlineData(14)]
        public void TestGetCharactersByType(int _typeId)
        {
            // Arrange
            List<Character> characters = new List<Character>
            {
                new CharacterObjectMother().ESTPCharacterNew(),
                new CharacterObjectMother().ISTJCharacterNew(),
                new CharacterObjectMother().ENTPCharacterNew()
            };

            var mockRepStub = new Mock<IRepositoryCharacter>();
            {
                mockRepStub.Setup(rep => rep.GetAll()).Returns(characters);
            }

            CharacterService characterService = new CharacterService(null, mockRepStub.Object);

            // Act
            List<CharacterModel> charactersList = characterService.GetCharacterByType(_typeId);

            // Assert
            Assert.NotNull(charactersList);
        }
    }
}
