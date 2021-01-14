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
    public class TestPostgreSQLUserRepository
    {
        private readonly User[] userCollection;
        private readonly IUser repository;

        public TestPostgreSQLUserRepository()
        {
            userCollection = TestRepositoryDataGenerator.GenerateTestUsers().Result;

            repository = new PostgreSQLUserRepository(CreateInMemoryContext());
        }

        #region SuccessTests
        [Fact]
        public async Task UserRepositoryReturnsUsers()
        {
            // Arrange
            var stub = new Mock<IUser>();
            stub.Setup(repo => repo.GetAll()).Returns(TestRepositoryDataGenerator.GenerateTestUsers());
            var userRepository = stub.Object;

            // Act
            var result = await userRepository.GetAll();

            // Assert
            Assert.IsType<User[]>(result);
            Assert.Equal(6, result.Length);
        }

        [Fact]
        public async Task UserRepositoryReturnsUserById()
        {
            // Arrange
            var stub = new Mock<IUser>();
            stub.Setup(repo => repo.GetById(3)).Returns(TestRepositoryDataGenerator.GenerateTestUser());
            var userRepository = stub.Object;

            // Act
            var result = await userRepository.GetById(3);

            // Assert
            Assert.IsType<User>(result);
            Assert.Equal("Peter", result.UserName);
            Assert.Equal(3, result.RoleId);
        }

        [Fact]
        public async Task UserRepositoryCreateUser()
        {
            // Arrange

            // Act
            await repository.Create(new UserBuilder().WithId(10).WithEmail("main@mail.ru").WithPassword("1245").Build());
            await repository.Save();

            var result = await repository.GetAll();

            // Assert
            Assert.IsType<User[]>(result);
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public async Task UserRepositoryUpdateUser()
        {
            // Arrange
            var oldValue = await repository.GetById(1);
            oldValue.UserName = "Ivan";

            // Act
            repository.Update(oldValue);
            await repository.Save();

            var result = await repository.GetById(1);

            // Assert
            Assert.IsType<User>(result);
            Assert.Equal("Ivan", result.UserName);
        }

        [Fact]
        public async Task UserRepositoryDeleteUser()
        {
            // Arrange

            // Act
            await repository.Delete(7);
            await repository.Save();

            var result = await repository.GetById(7);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region FailedTests
        [Fact]
        public async Task UserRepositoryReturnsNull()
        {
            // Arrange

            // Act
            var result = await repository.GetById(Int32.MaxValue);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Initialization
        private ShopichContext CreateInMemoryContext()
        {
            // Database' Name have to be unique in memory to prevent collisions
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShopichContext(options);

            context.AddRangeAsync(userCollection);
            context.SaveChanges();

            return context;
        }
        #endregion
    }
}