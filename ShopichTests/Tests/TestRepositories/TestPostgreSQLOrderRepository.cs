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
    public class TestPostgreSQLOrderRepository
    {
        public IOrder GenerateRepositoryWithOrders()
        {
            var orderCollection = TestRepositoryDataGenerator.GenerateTestOrders().Result;

            var repository = new PostgreSQLOrderRepository(CreateInMemoryContext(orderCollection));

            return repository;
        }

        public IOrder GenerateRepositoryWithOrder()
        {
            var orderCollection = TestRepositoryDataGenerator.GenerateTestOrder().Result;

            var repository = new PostgreSQLOrderRepository(CreateInMemoryContext(orderCollection));

            return repository;
        }

        #region SuccessTests
        [Fact]
        public async Task OrderRepositoryReturnsOrders()
        {
            // Arrange
            var stub = new Mock<IOrder>();
            stub.Setup(repo => repo.GetAll()).Returns(TestRepositoryDataGenerator.GenerateTestOrders());
            var orderRepository = stub.Object;

            // Act
            var result = await orderRepository.GetAll();

            // Assert
            Assert.IsType<Order[]>(result);
            Assert.Equal(6, result.Length);
        }

        [Fact]
        public async Task OrderRepositoryReturnsCategoryById()
        {
            // Arrange
            var stub = new Mock<IOrder>();
            stub.Setup(repo => repo.GetById(3)).Returns(TestRepositoryDataGenerator.GenerateTestOrder());
            var orderRepository = stub.Object;

            // Act
            var result = await orderRepository.GetById(3);

            // Assert
            Assert.IsType<Order>(result);
            Assert.Equal(false, result.IsApproved);
        }
        [Fact]
        public async Task OrderRepositoryCreateOrder()
        {
            // Arrange
            var repository = GenerateRepositoryWithOrders();

            // Act
            await repository.Create(new OrderBuilder().WithId(10).WithDate("2021-01-14").WithIsApproved(true).Build());
            await repository.Save();

            var result = await repository.GetAll();

            // Assert
            Assert.IsType<Order[]>(result);
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public async Task OrderRepositoryUpdateOrder()
        {
            // Arrange
            var repository = GenerateRepositoryWithOrders();
            var oldValue = await repository.GetById(1);
            oldValue.IsApproved = true;

            // Act
            repository.Update(oldValue);
            await repository.Save();

            var result = await repository.GetById(1);

            // Assert
            Assert.IsType<Order>(result);
            Assert.Equal(true, result.IsApproved);
        }

        [Fact]
        public async Task OrderRepositoryDeleteOrder()
        {
            // Arrange
            var repository = GenerateRepositoryWithOrder();

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
            var repository = GenerateRepositoryWithOrders();

            // Act
            var result = await repository.GetById(Int32.MaxValue);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Initialization
        private ShopichContext CreateInMemoryContext(Order[] orderCollection)
        {
            // Database' Name have to be unique in memory to prevent collisions
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShopichContext(options);

            context.AddRangeAsync(orderCollection);
            context.SaveChanges();

            return context;
        }

        private ShopichContext CreateInMemoryContext(Order orderCollection)
        {
            // Database' Name have to be unique in memory to prevent collisions
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShopichContext(options);

            context.AddRangeAsync(orderCollection);
            context.SaveChanges();

            return context;
        }
        #endregion
    }
}