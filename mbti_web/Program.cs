using Microsoft.EntityFrameworkCore;
using mbti_web.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using mbti_web;
using mbti_web.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });

*/


builder.Services.AddControllers();
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddSingleton<IRepositoryType, RepositoryType>();
builder.Services.AddSingleton<IRepositoryCharacter, RepositoryCharacter>();


string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<mbti_dbContext>(options => options.UseNpgsql(connection));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.UseJwtBearerAuthentication(options);


if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();