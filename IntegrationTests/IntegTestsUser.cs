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

namespace IntegrationTests
{
    [AllureSuite("IntegrationTestUserService")]
    public class IntegTestsUser: IClassFixture<WebApplicationFactory<mbti_web.Startup>> //, IDisposable
    {
        //private DbContextOptions<mbti_dbContext> options;
        /*private mbti_dbContext dbContext;
        private IRepositoryUser repUser;
        private IRepositoryType repType;
        private IRepositoryCharacter repCharacter;*/

        //private readonly WebApplicationFactory<Startup> _factory;
        private IUserService userService;
        public List<UserModel> data;

        //private IRepositoryUser sut { get { return repUser; } }

        /*public IntegTestsUserService()//(WebApplicationFactory<Startup> factory)
        {
             this._factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
             {
                 builder.ConfigureTestServices(services =>
                 {
                     services.AddDbContext<mbti_dbContext>(opt => opt.UseInMemoryDatabase("mydb"));

                     services.AddScoped<IRepositoryUser, RepositoryUser>();
                     services.AddScoped<IRepositoryType, RepositoryType>();
                     services.AddScoped<IRepositoryCharacter, RepositoryCharacter>();

                     services.AddScoped<IUserService, UserService>();
                     services.AddScoped<ITypeService, TypeService>();
                     services.AddScoped<ICharacterService, CharacterService>();

                     services.AddAutoMapper(typeof(UserProfile));
                     services.AddAutoMapper(typeof(TypeProfile));
                     services.AddAutoMapper(typeof(CharacterProfile));

                     var provider = services.BuildServiceProvider();
                     this.userService = provider.GetRequiredService<IUserService>();
                 });
             });
            //_factory = factory;
        }*/

        private readonly WebApplicationFactory<mbti_web.Startup> _factory;

        public IntegTestsUser(WebApplicationFactory<mbti_web.Startup> factory)
        {
            _factory = factory;
        }

        /*public IntegTestsUserService()
        {
            dbContext = new mbti_dbContext(new DbContextOptions<mbti_dbContext>());

            repUser = new RepositoryUser(dbContext);
            repType = new RepositoryType(dbContext);
            repCharacter = new RepositoryCharacter(dbContext);

            ClearTestDb();
        }

        private void ClearTestDb()
        {
            foreach (var record in repUser.GetAll())
            {
                repUser.Remove(record);
            }

            foreach (var record in repType.GetAll())
            {
                repType.Remove(record);
            }

            foreach (var record in repCharacter.GetAll())
            {
                repCharacter.Remove(record);
            }
        }
        public void Dispose()
        {
            ClearTestDb();
            dbContext.Dispose();
        }*/

        /*[AllureXunitTheory]
        [AutoMoqData]
        public void TestAddAndGetUser(User _user)
        {
            // Arrange
            User user = new User(_user.Useruk, _user.Login, _user.Password, _user.Nickname,
                                                    _user.Email, _user.Telagram, _user.Typeuk, _user.Dateofbirth);


            var userService = new UserService(this.repUser, null, null);
            sut.Add(user);

            // Act
            var userModel = userService.GetByLogin(user.Login);

            // Act
            Assert.Equal(user.Login, userModel.Login);
        }

        [AllureXunit]
        public void TestAuthorize()
        {
            // https://stackoverflow.com/questions/49934707/automapper-in-xunit-testing-and-net-core-2-0
            // использовать MapperConfiguration и InMemory configuration

            // Arrange
            var inMemorySettings = new Dictionary<string, string>
            {
                {"Secret", "0123456789123456"}
            };

            IConfiguration configurationInMemory = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            
            var mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
            configurationInMemory.Add
                RegisterInstance<IMapper>(mapper);

            UserService userService = new UserService(new RepositoryUser(), configurationInMemory, new );

            // Act

            // Assert
        }*/

        [Fact]
        public void TestRegister()
        {
            // Arrange
            //var client = _factory.CreateClient();

            IUserService s = null;

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<mbti_dbContext>(opt => opt.UseInMemoryDatabase("mydb"));

                    var serviceProvider = services.BuildServiceProvider();

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        s = scopedServices
                            .GetRequiredService<IUserService>();
                        Assert.Null(s);
                    }
                
                });
            });

            //Assert.NotNull(s);


            //var provider = services.BuildServiceProvider();
            //var serv = provider.GetRequiredService<IUserService>();

            // Act
            //var response = await client.GetAsync("/api/types");
            //var responce = userService.GetAll();

            // Assert
            //Assert.NotNull(responce);
            //Assert.NotNull(response);//response.EnsureSuccessStatusCode(); // Status Code 200-299

            // с postman
            // wireshark - для лога
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