using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using mbti_web;
using Allure.Xunit.Attributes;
using mbti_web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace E2ETest
{
    public class E2ETest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly string _baseUrl = "https://localhost:7026/api/";
        private readonly HttpClient _client;
        public E2ETest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _client.BaseAddress = new Uri(_baseUrl);
        }

        [AllureXunit]
        public async Task DemoScenarioTest()
        {
            // [Registration]

            // Arrange
            var regData = new AuthenticateRequest()
            {
                Login = "lenka",
                Password = "1234"
            };

            var regContent = JsonContent.Create(regData);

            // Act
            var response = await _client.PostAsync(_baseUrl + "users/register", regContent);
            var regRes = await response.Content.ReadFromJsonAsync<UserModel>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(regData.Login, regRes.Login);
            Assert.Equal(regData.Password, regRes.Password);

            // [Authorization]

            // Arrange
            var authData = new AuthenticateRequest()
            {
                Login = "lenka",
                Password = "1234"
            };

            var authContent = JsonContent.Create(authData);

            // Act
            response = await _client.PostAsync(_baseUrl + "users/authenticate", authContent);
            var authRes = await response.Content.ReadFromJsonAsync<AuthenticateResponse>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(authRes.Token);


            // [Get info about types]

            // Arrange
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authRes.Token);
            int cntTypes = 16;

            // Act
            response = await _client.GetAsync(_baseUrl + "types");
            var typesRes = await response.Content.ReadFromJsonAsync<List<TypeModel>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(cntTypes, typesRes.Count);
            Assert.NotNull(typesRes);


            // [Get info about characters]

            // Arrange

            // Act
            response = await _client.GetAsync(_baseUrl + "characters");
            var charactersRes = await response.Content.ReadFromJsonAsync<List<CharacterModel>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(charactersRes);

            // [Find characters with ISTJ type]

            // Arrange
            string type = "ISTJ";

            // Act
            response = await _client.GetAsync(_baseUrl + "characters?str=" + type + "&flag=1");
            var charactersISTJRes = await response.Content.ReadFromJsonAsync<List<CharacterModel>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(type, charactersISTJRes[0].Type);
            Assert.NotNull(charactersISTJRes);


            // Clear
            await _client.DeleteAsync(_baseUrl + "users/" + authRes.ID.ToString());
        }
    }
}