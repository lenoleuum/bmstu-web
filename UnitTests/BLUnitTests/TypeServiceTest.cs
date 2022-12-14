using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mbti_web.Models;
using mbti_web.Repository;
using mbti_web.Entities;
using mbti_web.Services;
using System;
using UnitTests.DataBuilders;
using mbti_web;
using Moq;
using Allure.Xunit.Attributes;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using UnitTests.ObjectMother;

using Type = mbti_web.Entities.Type;

namespace UnitTests.BLUnitTests
{
    [AllureSuite("TypeServiceSuite")]
    public class TypeServiceTest
    {
        [AllureXunitTheory]
        [AutoMoqData]
        public void TestGetById(int _id)
        {
            // Arrange
            Type testType = new TypeObjectMother().INTJType();

            var mockRepStub = new Mock<IRepositoryType>();
            {
                mockRepStub.Setup(rep => rep.Find(testType.Typeuk)).Returns(testType);
            }

            TypeService typeService = new TypeService(null, mockRepStub.Object);

            // Act
            TypeModel actualType = typeService.GetTypeByID(testType.Typeuk);

            // Assert
            Assert.Null(actualType);
        }

        //[AllureXunit]
        public void TestGetAllTypes()
        {
            // Arrange
            List<Type> types = new List<Type> { new TypeObjectMother().ENTPType() };

            var mockRepStub = new Mock<IRepositoryType>();
            {
                mockRepStub.Setup(rep => rep.GetAll()).Returns(types);
            }

            TypeService typeService = new TypeService(null, mockRepStub.Object);

            // Act
            List<TypeModel> typeList = typeService.GetAllTypes().ToList();

            // Assert
            Assert.Null(typeList);
        }

        [AllureXunitTheory]
        [AutoMoqData]
        public void TestGetTypeByName(string _name)
        {
            // Arrange
            List<Type> types = new List<Type> { new TypeObjectMother().INTJType() };

            var mockRepStub = new Mock<IRepositoryType>();
            {
                mockRepStub.Setup(rep => rep.GetAll()).Returns(types);
            }

            TypeService typeService = new TypeService(null, mockRepStub.Object);

            // Act
            TypeModel? actualType = typeService.GetTypeByName(_name);

            // Assert
            Assert.Null(actualType);
        }
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
