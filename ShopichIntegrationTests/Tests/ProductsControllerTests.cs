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
using Shopich.Controllers.api;

namespace ShopichIntegrationTests.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task GetSomeProductsTestsResponse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(databaseName: "WithProducts")
                .Options;
            var products = await TestDataGenerator.GenerateTestProducts();
            var firstContext = new ShopichContext(options);
            firstContext.Products.AddRange(products);
            firstContext.SaveChanges();

            var secondContext = new ShopichContext(options);
            var repository = new PostgreSQLProductRepository(secondContext);

            // Act
            var result = await repository.GetAll();

            // Assert
            result.Should().HaveCount(6);
        }

        [Fact]
        public async Task GetEmptyReviewsTestsResponse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(databaseName: "WithoutProducts")
                .Options;
            var firstContext = new ShopichContext(options);

            var repository = new PostgreSQLProductRepository(firstContext);

            // Act
            var result = await repository.GetAll();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
