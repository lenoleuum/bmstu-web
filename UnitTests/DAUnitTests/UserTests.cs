using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using mbti_web.Entities;
using mbti_web;
using mbti_web.Repository;
using UnitTests.DataBuilders;
using Allure.Xunit.Attributes;
using UnitTests.ObjectMother;
using UnitTests.Helpers;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using mbti_web.Repository.Exceptions;

namespace UnitTests
{
    [AllureSuite("UserRepositorySuite")]
    public class UserTests
    {
        private mbti_dbContext _contextMock;

        // Setup
        public UserTests()
        {
            _contextMock = new MemoryContext().GetMemoryContext();

            _contextMock.Database.EnsureDeleted();

            _contextMock.Users.Add(new UserObjectMother().AuthorizedUser());
            _contextMock.Users.Add(new UserObjectMother().ExistingUserLena());
            _contextMock.Users.Add(new UserObjectMother().ExistingUserMasha());
            _contextMock.Users.Add(new UserObjectMother().UserForDelete());

            _contextMock.SaveChanges();
        }

        [AllureXunitTheory]
        [InlineData(1, "lena", "123", "lena", "gusjushka@gmail.com", "@helena.fro", 8)]
        public void TestGetById(int _id, string _login, string _password, string _nickname,
                                    string _email, string _telegram, int _typeUk)
        {
            // Arrange
            DateTime dateOfBirth = DateTime.ParseExact("2002-01-01", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            User expectedUser = new UserBuilder()
                                .WithId(_id)
                                .WithLogin(_login)
                                .WithPassword(_password)
                                .WithNickname(_nickname)
                                .WithEmail(_email)
                                .WithTelegram(_telegram)
                                .WithDateOfBirth(dateOfBirth)
                                .WithType(_typeUk)
                                .Build();

            RepositoryUser repUser = new RepositoryUser(_contextMock);

            // Act
            User actualUser = repUser.Find(_id);

            // Assert
            Assert.Equal(expectedUser.Useruk, actualUser.Useruk);
            Assert.Equal(expectedUser.Login, actualUser.Login);
            Assert.Equal(expectedUser.Password, actualUser.Password);
            Assert.Equal(expectedUser.Nickname, actualUser.Nickname);
            Assert.Equal(expectedUser.Email, actualUser.Email);
            Assert.Equal(expectedUser.Telagram, actualUser.Telagram);
            Assert.Equal(expectedUser.Dateofbirth, actualUser.Dateofbirth);
            Assert.Equal(expectedUser.Typeuk, actualUser.Typeuk);
        }

        [AllureXunitTheory]
        [AutoMoqData]
        public void TestGetByIdNotExist(int _id)
        {
            // Arrange
            RepositoryUser repUser = new RepositoryUser(_contextMock);

            // Act
            Action throwingAction = () => repUser.Find(_id);

            // Assert
            var ex = Assert.Throws<UserNotFoundException>(throwingAction);
            //Assert.Contains(_id.ToString(), ex.Message);
        }

        [AllureXunit]
        public void TestGetAll()
        {
            // Arrange
            int expectedCount = _contextMock.Users.Count();
            RepositoryUser repUser = new RepositoryUser(_contextMock);

            // Act
            List<User> userList = repUser.GetAll().ToList();
            int actualCount = userList.Count;

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [AllureXunit]
        public void TestAdd()
        {
            // Arrange
            string login = "testuser";
            string password = "test123";
            User expectedUser = new UserBuilder()
                                .WithLogin(login)
                                .WithPassword(password)
                                .Build();

            RepositoryUser repUser = new RepositoryUser(_contextMock);

            // Act
            repUser.Add(expectedUser);
            User? actualUser = _contextMock.Users.Where(u => u.Useruk == expectedUser.Useruk).FirstOrDefault();

            // Assert
            Assert.Equal(expectedUser.Login, actualUser.Login);
            Assert.Equal(expectedUser.Password, actualUser.Password);
            Assert.Equal(expectedUser.Nickname, actualUser.Nickname);
            Assert.Equal(expectedUser.Email, actualUser.Email);
            Assert.Equal(expectedUser.Telagram, actualUser.Telagram);
            Assert.Equal(expectedUser.Dateofbirth, actualUser.Dateofbirth);
            Assert.Equal(expectedUser.Typeuk, actualUser.Typeuk);
        }

        [AllureXunitTheory]
        [InlineData(1, "lena")]
        public void TestAddRepeat(int _id, string _login)
        {
            // Arrange
            User newUser = new UserBuilder()
                                .WithId(_id)
                                .WithLogin(_login)
                                .Build();

            RepositoryUser repUser = new RepositoryUser(_contextMock);

            // Act
            Action throwingAction = () => repUser.Add(newUser);

            // Assert
            var ex = Assert.Throws<UserAddException>(throwingAction);
            Assert.Contains(_login, ex.Message);
        }

        [AllureXunitTheory]
        [InlineData(1, "lena", "123", "lena", "gusjushka@gmail.com", "@helena.fro", 8)] // todo: inline data списком
        public void TestUpdate(int _id, string _login, string _password, string _nickname,
                                    string _email, string _telegram, int _typeUk)
        {
            // Arrange
            DateTime dateOfBirth = DateTime.ParseExact("2002-01-01", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            User expectedUser = new UserBuilder()
                                .WithId(_id)
                                .WithLogin(_login)
                                .WithPassword(_password)
                                .WithNickname(_nickname)
                                .WithEmail(_email)
                                .WithTelegram(_telegram)
                                .WithDateOfBirth(dateOfBirth)
                                .WithType(_typeUk)
                                .Build();
            expectedUser.Email = "newemail@gmail.com";
            expectedUser.Telagram = "@newtelegram";

            RepositoryUser repUser = new RepositoryUser(_contextMock);

            // Act
            repUser.Update(expectedUser);
            User? actualUser = _contextMock.Users.Where(u => u.Useruk == _id).FirstOrDefault();

            // Assert
            Assert.Equal(expectedUser.Email, actualUser.Email);
            Assert.Equal(expectedUser.Telagram, actualUser.Telagram);
        }

        [AllureXunitTheory]
        [AutoMoqData]
        public void TestUpdateNotExist(int _id)
        {
            // Arrange
            User newUser = new UserBuilder()
                                    .WithId(_id)
                                    .Build();
            newUser.Email = "lenoleuum@gmail.com";
            newUser.Telagram = "@lenoleeum";

            RepositoryUser repUser = new RepositoryUser(_contextMock);

            // Act
            Action throwingAction = () => repUser.Update(newUser);

            // Assert
            var ex = Assert.Throws<UserUpdateException>(throwingAction);
            Assert.Contains(_id.ToString(), ex.Message);
        }

        [AllureXunitTheory]
        [InlineData(100000)]
        public void TestDelete(int _id)
        {
            // Arrange
            User expectedUser = new UserBuilder()
                                    .WithId(_id)
                                    .Build();

            RepositoryUser repUser = new RepositoryUser(_contextMock);

            // Act
            repUser.Remove(expectedUser);
            User? actualUser = _contextMock.Users.Where(u => u.Useruk == expectedUser.Useruk).FirstOrDefault();

            // Assert
            Assert.Null(actualUser);
        }

        [AllureXunitTheory]
        [AutoMoqData]
        public void TestDeleteNotExist(int _id)
        {
            // Arrange
            User deleteUser = new UserBuilder()
                                    .WithId(_id)
                                    .Build();

            RepositoryUser repUser = new RepositoryUser(_contextMock);

            // Act
            Action throwingAction = () => repUser.Remove(deleteUser);

            // Assert
            var ex = Assert.Throws<UserDeleteException>(throwingAction);
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
