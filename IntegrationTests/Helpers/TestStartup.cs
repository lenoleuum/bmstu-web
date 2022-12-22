using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mbti_web;
using mbti_web.Models;
using mbti_web.Repository;
using mbti_web.Entities;
using mbti_web.Controllers;
using mbti_web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Helpers
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
            configuration["Secret"] = "0123456789123456";
            configuration["DefaultConnection"] = "Host=localhost; Port=5432;Database=mbti_db;Username=postgres;Password=1234;";
        }
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<mbti_dbContext>(opt =>
                opt.UseNpgsql(Configuration["DefaultConnection"]),
                ServiceLifetime.Transient);

            services.AddScoped<IRepositoryUser, RepositoryUser>();
            services.AddScoped<IRepositoryType, RepositoryType>();
            services.AddScoped<IRepositoryCharacter, RepositoryCharacter>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<ICharacterService, CharacterService>();

            services.AddControllers();

            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(TypeProfile));
            services.AddAutoMapper(typeof(CharacterProfile));
            services.AddCors();
        }
    }
}
