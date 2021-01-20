using FluentAssertions;
using Nancy.Json;
using Newtonsoft.Json;
using Shopich.Models;
using ShopichTests.DataBuilders;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopichIntegrationTests.Tests.WithMocks 
{
    public class AuthControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetInfoAboutSelfTestsResponse()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("api/v1/users/1");
            var result = await response.Content.ReadAsAsync<User>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.UserEmail.Should().Be("test@integration.com");
        }

        [Fact]
        public async Task RegisterTestsResponse()
        {
            // Arrange
            var user = new RegisterBuilder().WithEmail("test@test.ru").WithPassword("12345").Build();
            var stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync("api/v1/auth/register", stringContent);
            var result = await response.Content.ReadAsAsync<User>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            result.UserEmail.Should().Be("test@test.ru");
        }
    }
}
