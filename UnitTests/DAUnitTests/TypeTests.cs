using Xunit;
using System;
using mbti_web.Repository;
using mbti_web.Repository.Exceptions;
using mbti_web;
using UnitTests.DataBuilders;
using System.Collections.Generic;
using System.Linq;
using Allure.Xunit.Attributes;
using Type = mbti_web.Entities.Type;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTests.ObjectMother;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using UnitTests.Helpers;

namespace UnitTests
{
    [AllureSuite("TypeRepositorySuite")]
    public class TypeTests
    {
        private mbti_dbContext _contextMock;

        // Setup
        public TypeTests()
        {
            _contextMock = new MemoryContext().GetMemoryContext();

            _contextMock.Database.EnsureDeleted();

            _contextMock.Types.Add(new TypeObjectMother().ENTPType());
            _contextMock.Types.Add(new TypeObjectMother().INTJType());
            _contextMock.Types.Add(new TypeObjectMother().INTPType());
            _contextMock.Types.Add(new TypeObjectMother().ENTJType());

            _contextMock.SaveChanges();
        }

        [AllureXunitTheory]
        [InlineData(4, "INTP", "A Logician (INTP) is someone with the Introverted, Intuitive, Thinking, and Prospecting personality traits. These flexible thinkers enjoy taking an unconventional approach to many aspects of life. They often seek out unlikely paths, mixing willingness to experiment with personal creativity.")]
        public void TestGetById(int _id, string _name, string _description)
        {
            // Arrange
            Type expectedType = new TypeBuilder()
                                    .WithId(_id)
                                    .WithName(_name)
                                    .WithDescription(_description)
                                    .Build();

            RepositoryType repType = new RepositoryType(_contextMock);

            // Act
            Type actualType = repType.Find(_id);


            // Assert
            Assert.Equal(expectedType.Typeuk, actualType.Typeuk);
            Assert.Equal(expectedType.Typename, actualType.Typename);
            Assert.Equal(expectedType.Typedescription, actualType.Typedescription);
        }

        [AllureXunitTheory]
        [AutoMoqData]
        public void TestGetByIdNotExist(int _id)
        {
            // Arrange
            RepositoryType repType = new RepositoryType(_contextMock);

            // Act
            Action throwingAction = () => repType.Find(_id);

            // Assert
            var ex = Assert.Throws<TypeNotFoundException>(throwingAction);
            Assert.Contains(_id.ToString(), ex.Message);
        }

        [AllureXunit]
        public void TestGetAll()
        {
            // Arrange
            int expectedCount = _contextMock.Types.Count();
            RepositoryType repType = new RepositoryType(_contextMock);

            // Act
            List<Type> typesList = repType.GetAll().ToList();
            int actualCount = typesList.Count;

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        public class AutoMoqDataAttribute : AutoDataAttribute
        {
            public AutoMoqDataAttribute()
#pragma warning disable CS0618 // Type or member is obsolete
            : base(new AutoFixture.Fixture().Customize(new AutoMoqCustomization()))
#pragma warning restore CS0618 // Type or member is obsolete
            { }
        }
        protected void Dispose(bool disposing)
        {
            _contextMock.Database.EnsureDeleted();

            if (disposing)
                _contextMock.Dispose();
        }
    }
}