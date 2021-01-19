using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Shopich;
using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopichEndToEndTests;
using Shopich.ViewModels;
using System.Net.Http.Headers;

namespace ShopichIntegrationTests.Tests.WithMocks 
{
    public class EndToEndTests
    {
        [Fact]
        public async Task GetReviewsScenario()
        {
            // Arrange
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services => {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(DbContextOptions<ShopichContext>));
                        services.Remove(descriptor);
                        services.AddDbContext<ShopichContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });
            var TestClient = appFactory.CreateClient();

            using (var scope = appFactory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ShopichContext>();
                context.Roles.AddRange(await TestDataGenerator.GenerateTestRoles());
                context.Users.AddRange(await TestDataGenerator.GenerateTestUsers());
                context.Products.AddRange(await TestDataGenerator.GenerateTestProducts());
                context.Categories.AddRange(await TestDataGenerator.GenerateTestCategories());
                context.CategoryCollection.AddRange(await TestDataGenerator.GenerateTestCategoriesCollection());
                context.OrderCollection.AddRange(await TestDataGenerator.GenerateTestOrders());
                context.Reviews.AddRange(await TestDataGenerator.GenerateTestReviews());
                context.Orders.AddRange(await TestDataGenerator.GenerateTestOrdersCollection());
                context.SaveChanges();
            }

            var newUser = new RegisterModel { email = "test@test.ru", password = "12345" };
            var loginInfo = new LoginModel { Email = "test@test.ru", Password = "12345" };
            var newUserInString = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
            var loginInfoInString = new StringContent(JsonConvert.SerializeObject(loginInfo), Encoding.UTF8, "application/json");

            // Act
            var registerResponse = await TestClient.PostAsync("api/v1/auth/register", newUserInString);
            var registerResult = await registerResponse.Content.ReadAsAsync<User>();

            // Assert
            registerResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            registerResult.UserEmail.Should().Be("test@test.ru");

            // Act
            var loginResponse = await TestClient.PostAsync("api/v1/auth/login", loginInfoInString);
            var loginResult = await loginResponse.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(loginResult);
            var token = values["access_token"];
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Assert
            loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act
            var productsResponse = await TestClient.GetAsync("api/v1/products");
            var productsResult = await productsResponse.Content.ReadAsAsync<Product[]>();

            // Assert
            productsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            productsResult.Length.Should().Be(5);

            // Act
            var productResponse = await TestClient.GetAsync("api/v1/products/3");
            var productResult = await productResponse.Content.ReadAsAsync<Product>();

            // Assert
            productResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            productResult.ProductId.Should().Be(3);
            productResult.ProductName.Should().Be("Beans");

            // Act
            var reviewsResponse = await TestClient.GetAsync("api/v1/products/3/reviews");
            var reviewResult = await reviewsResponse.Content.ReadAsAsync<Review[]>();

            // Assert
            reviewsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            reviewResult.Length.Should().Be(1);
            reviewResult[0].ReviewText.Should().Be("Awesome");
        }
    }
}
