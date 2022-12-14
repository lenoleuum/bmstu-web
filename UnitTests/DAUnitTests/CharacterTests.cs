using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using mbti_web.Entities;
using mbti_web;
using mbti_web.Repository;
using mbti_web.Repository.Exceptions;
using UnitTests.DataBuilders;
using Allure.Xunit.Attributes;
using UnitTests.ObjectMother;
using Microsoft.EntityFrameworkCore;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using UnitTests.Helpers;


namespace UnitTests
{
    [AllureSuite("CharacterRepositorySuite")]
    public class CharacterTests
    {
        private mbti_dbContext _contextMock;

        // Setup
        public CharacterTests()
        {
            _contextMock = new MemoryContext().GetMemoryContext();

            _contextMock.Database.EnsureDeleted();

            _contextMock.Characters.Add(new CharacterObjectMother().ESTPCharacterNew());
            _contextMock.Characters.Add(new CharacterObjectMother().INFJCharacterExist());
            _contextMock.Characters.Add(new CharacterObjectMother().ENTPCharacterNew());
            _contextMock.Characters.Add(new CharacterObjectMother().ENFJCharacterNew());

            _contextMock.SaveChanges();
        }

        [AllureXunitTheory]
        [InlineData(1, "Armin Arlert", 8, "Anime")]
        public void TestGetById(int _id, string _name, int _type, string _category)
        {
            // Arrange
            Character expectedCharacter = new CharacterBuilder()
                                            .WithId(_id)
                                            .WithName(_name)
                                            .WithType(_type)
                                            .WithCategory(_category)
                                            .Build();

            // Act
            Character actualCharacter = new RepositoryCharacter(_contextMock).Find(_id);

            // Assert
            Assert.Equal(expectedCharacter.Characteruk, actualCharacter.Characteruk);
            Assert.Equal(expectedCharacter.Charactername, actualCharacter.Charactername);
            Assert.Equal(expectedCharacter.Typeuk, actualCharacter.Typeuk);
            Assert.Equal(expectedCharacter.Category, actualCharacter.Category);
        }

        [AllureXunitTheory]
        [AutoMoqData]
        public void TestGetByIdNotExist(int _id)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<mbti_dbContext>()
                                .UseInMemoryDatabase(databaseName: "MBTIdb")
                                .Options;

            RepositoryCharacter repCharacter = new RepositoryCharacter(_contextMock);

            // Act
            Action throwingAction = () => repCharacter.Find(_id);

            // Assert
            var ex = Assert.Throws<CharacterNotFoundException>(throwingAction);
            Assert.Contains(_id.ToString(), ex.Message);
        }

        [AllureXunit]
        public void TestGetAll()
        {
            // Arrange
            int expectedCount = _contextMock.Characters.Count();

            // Act
            List<Character> characterList = new RepositoryCharacter(_contextMock).GetAll().ToList();
            int actualCount = characterList.Count;

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [AllureXunitTheory]
        public void TestUpdateTypeUk()
        {
            // Arrange
            Character oldCharacter = new CharacterObjectMother().INFJCharacterExist();

            RepositoryCharacter repCharacter = new RepositoryCharacter(_contextMock);

            // Act
            repCharacter.Update(oldCharacter);
            Character? newCharacter = _contextMock.Characters.Where(c => c.Characteruk == oldCharacter.Characteruk).FirstOrDefault();

            // Assert
            Assert.Equal(oldCharacter.Typeuk, newCharacter.Typeuk);
        }

        [AllureXunitTheory]
        [AutoMoqData]
        public void TestUpdateTypeUkNotExist(int _typeUk)
        {
            // Arrange
            Character oldCharacter = new CharacterBuilder()
                                        .WithId(_typeUk)
                                        .Build();

            RepositoryCharacter repCharacter = new RepositoryCharacter(_contextMock);

            // Act
            Action throwingAction = () => repCharacter.Update(oldCharacter);

            // Assert
            var ex = Assert.Throws<CharacterUpdateException>(throwingAction);
            Assert.Contains(oldCharacter.Characteruk.ToString(), ex.Message);
        }

        [AllureXunit]
        public void TestAddCharacter()
        {
            // Arrange
            Character expectedCharacter = new CharacterObjectMother().ISTJCharacterNew();

            RepositoryCharacter repCharacter = new RepositoryCharacter(_contextMock);

            // Act
            repCharacter.Add(expectedCharacter);
            Character? actualCharacter = _contextMock.Characters.Where(c => c.Characteruk == expectedCharacter.Characteruk).FirstOrDefault();

            // Assert
            Assert.Equal(expectedCharacter.Characteruk, actualCharacter.Characteruk);
            Assert.Equal(expectedCharacter.Charactername, actualCharacter.Charactername);
            Assert.Equal(expectedCharacter.Typeuk, actualCharacter.Typeuk);
            Assert.Equal(expectedCharacter.Category, actualCharacter.Category);
        }

        [AllureXunitTheory]
        [InlineData(1, "Armin Arlert", 8, "Anime")]
        public void TestAddCharacterExists(int _id, string _name, int _type, string _category)
        {
            // Arrange
            Character newCharacter = new CharacterBuilder()
                                            .WithId(_id)
                                            .WithName(_name)
                                            .WithType(_type)
                                            .WithCategory(_category)
                                            .Build();

            RepositoryCharacter repCharacter = new RepositoryCharacter(_contextMock);

            // Act
            Action throwingAction = () => repCharacter.Add(newCharacter);

            // Assert
            var ex = Assert.Throws<CharacterAddException>(throwingAction);
            Assert.Contains(_id.ToString(), ex.Message);
        }
        public class AutoMoqDataAttribute : AutoDataAttribute
        {
            public AutoMoqDataAttribute()
#pragma warning disable CS0618 // Type or member is obsolete
            : base(new AutoFixture.Fixture().Customize(new AutoMoqCustomization()))
#pragma warning restore CS0618 // Type or member is obsolete
            { }
        }

        // Clear
        protected void Dispose(bool disposing)
        {
            _contextMock.Database.EnsureDeleted();

            if (disposing)
                _contextMock.Dispose();
        }
    }
}
