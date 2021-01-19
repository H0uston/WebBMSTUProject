using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Shopich;
using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Moq;
using Shopich.Repositories.interfaces;
using Shopich.Repositories.implementations;

namespace ShopichIntegrationTests.Tests
{
    public class ReviewControllerTests : IntegrationTest
    {
        public async Task GetAllCategoriesTestsResponse()
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
                        var productRepo = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(IProduct));

                        var stub = new Mock<IProduct>();
                        stub.Setup(repo => repo.GetAll()).Returns(TestDataGenerator.GenerateTestProducts());
                        // services.AddScoped<stub.Object, PostgreSQLProductRepository>(stub.Object);
                    });
                });

            var TestClient = appFactory.CreateClient();

            using (var scope = appFactory.Services.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IProduct>();
                // stub = new Mock<IProduct>();

            }
            
            // Act
            var response = await TestClient.GetAsync("api/v1/categories");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<IEnumerable<Category>>()).Should().BeEmpty();
        }

    }
}
