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
    public class TestPostgreSQLOrdersRepository
    {
        public IOrders GenerateRepositoryWithOrdersCollection()
        {
            var ordersCollection = TestRepositoryDataGenerator.GenerateTestOrdersCollection().Result;

            var repository = new PostgreSQLOrdersRepository(CreateInMemoryContext(ordersCollection));

            return repository;
        }

        public IOrders GenerateRepositoryWithOrdersSingle()
        {
            var ordersCollection = TestRepositoryDataGenerator.GenerateTestOrdersSingle().Result;

            var repository = new PostgreSQLOrdersRepository(CreateInMemoryContext(ordersCollection));

            return repository;
        }

        #region SuccessTests
        [Fact]
        public async Task OrderRepositoryReturnsOrders()
        {
            // Arrange
            var stub = new Mock<IOrders>();
            stub.Setup(repo => repo.GetAll()).Returns(TestRepositoryDataGenerator.GenerateTestOrdersCollection());
            var orderRepository = stub.Object;

            // Act
            var result = await orderRepository.GetAll();

            // Assert
            Assert.IsType<Orders[]>(result);
            Assert.Equal(6, result.Length);
        }

        [Fact]
        public async Task OrderRepositoryReturnsCategoryById()
        {
            // Arrange
            var stub = new Mock<IOrders>();
            stub.Setup(repo => repo.GetById(3)).Returns(TestRepositoryDataGenerator.GenerateTestOrdersSingle());
            var orderRepository = stub.Object;

            // Act
            var result = await orderRepository.GetById(3);

            // Assert
            Assert.IsType<Orders>(result);
            Assert.Equal(10, result.Count);
        }
        [Fact]
        public async Task OrderRepositoryCreateOrder()
        {
            // Arrange
            var repository = GenerateRepositoryWithOrdersCollection();

            // Act
            await repository.Create(new OrdersBuilder().WithId(10).Build());
            await repository.Save();

            var result = await repository.GetAll();

            // Assert
            Assert.IsType<Orders[]>(result);
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public async Task OrderRepositoryUpdateOrder()
        {
            // Arrange
            var repository = GenerateRepositoryWithOrdersCollection();
            var oldValue = await repository.GetById(1);
            oldValue.Count = 20;

            // Act
            repository.Update(oldValue);
            await repository.Save();

            var result = await repository.GetById(1);

            // Assert
            Assert.IsType<Orders>(result);
            Assert.Equal(20, result.Count);
        }

        [Fact]
        public async Task OrderRepositoryDeleteOrder()
        {
            // Arrange
            var repository = GenerateRepositoryWithOrdersSingle();

            // Act
            var oldOrders = await repository.GetAll();
            var result = await repository.Delete(1);
            await repository.Save();
            var newOrders = await repository.GetAll();

            // Assert
            Assert.Single(oldOrders);
            Assert.Empty(newOrders);
            Assert.Equal(1, result);
        }
        #endregion

        #region FailedTests
        [Fact]
        public async Task OrderRepositoryReturnsNull()
        {
            // Arrange
            var repository = GenerateRepositoryWithOrdersCollection();

            // Act
            var result = await repository.GetById(Int32.MaxValue);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Initialization
        private ShopichContext CreateInMemoryContext(Orders[] ordersCollection)
        {
            // Database' Name have to be unique in memory to prevent collisions
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShopichContext(options);

            context.AddRangeAsync(ordersCollection);
            context.SaveChanges();

            return context;
        }

        private ShopichContext CreateInMemoryContext(Orders ordersCollection)
        {
            // Database' Name have to be unique in memory to prevent collisions
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShopichContext(options);

            context.AddRangeAsync(ordersCollection);
            context.SaveChanges();

            return context;
        }
        #endregion
    }
}