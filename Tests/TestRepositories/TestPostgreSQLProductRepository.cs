using Microsoft.EntityFrameworkCore;
using Moq;
using Shopich.Models;
using Shopich.Repositories.implementations;
using Shopich.Repositories.interfaces;
using ShopichTests.DataBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShopichTests.Tests.TestRepositories
{
    public class TestPostgreSQLProductRepository
    {
        public IProduct GenerateRepositoryWithProducts()
        {
            var productCollection = TestRepositoryDataGenerator.GenerateTestProducts().Result;

            var repository = new PostgreSQLProductRepository(CreateInMemoryContext(productCollection));

            return repository;
        }

        public IProduct GenerateRepositoryWithProduct()
        {
            var productCollection = TestRepositoryDataGenerator.GenerateTestProduct().Result;

            var repository = new PostgreSQLProductRepository(CreateInMemoryContext(productCollection));

            return repository;
        }

        #region SuccessTests
        [Fact]
        public async Task ProductRepositoryReturnsProduct()
        {
            // Arrange
            var stub = new Mock<IProduct>();
            stub.Setup(repo => repo.GetAll()).Returns(TestRepositoryDataGenerator.GenerateTestProducts());
            var productRepository = stub.Object;

            // Act
            var result = await productRepository.GetAll();

            // Assert
            Assert.IsType<Product[]>(result);
            Assert.Equal(6, result.Length);
        }

        [Fact]
        public async Task ProductRepositoryReturnsProductById()
        {
            // Arrange
            var stub = new Mock<IProduct>();
            stub.Setup(repo => repo.GetById(3)).Returns(TestRepositoryDataGenerator.GenerateTestProduct());
            var productRepository = stub.Object;

            // Act
            var result = await productRepository.GetById(3);

            // Assert
            Assert.IsType<Product>(result);
            Assert.Equal("Potato", result.ProductName);
        }

        [Fact]
        public async Task ProductRepositoryCreateProduct()
        {
            // Arrange
            var repository = GenerateRepositoryWithProducts();

            // Act
            await repository.Create(new ProductBuilder().WithId(10).WithName("Kuban Tomato").Build());
            await repository.Save();

            var result = await repository.GetAll();

            // Assert
            Assert.IsType<Product[]>(result);
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public async Task ProductRepositoryUpdateProduct()
        {
            // Arrange
            var repository = GenerateRepositoryWithProducts();
            var oldValue = await repository.GetById(1);
            oldValue.ProductName = "Cucumber";

            // Act
            repository.Update(oldValue);
            await repository.Save();

            var result = await repository.GetById(1);

            // Assert
            Assert.IsType<Product>(result);
            Assert.Equal("Cucumber", result.ProductName);
        }

        [Fact]
        public async Task ProductRepositoryDeleteProduct()
        {
            // Arrange
            var repository = GenerateRepositoryWithProduct();

            // Act
            var oldProducts = await repository.GetAll();
            var result = await repository.Delete(1);
            await repository.Save();
            var newProducts = await repository.GetAll();

            // Assert
            Assert.Single(oldProducts);
            Assert.Empty(newProducts);
            Assert.Equal(1, result);
        }
        #endregion

        #region FailedTests
        [Fact]
        public async Task ProductRepositoryReturnsNull()
        {
            // Arrange
            var repository = GenerateRepositoryWithProducts();

            // Act
            var result = await repository.GetById(Int32.MaxValue);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Initialization
        private ShopichContext CreateInMemoryContext(Product[] productCollection)
        {
            // Database' Name have to be unique in memory to prevent collisions
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShopichContext(options);

            context.AddRangeAsync(productCollection);
            context.SaveChanges();

            return context;
        }

        private ShopichContext CreateInMemoryContext(Product productCollection)
        {
            // Database' Name have to be unique in memory to prevent collisions
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShopichContext(options);

            context.AddRangeAsync(productCollection);
            context.SaveChanges();

            return context;
        }
        #endregion
    }
}