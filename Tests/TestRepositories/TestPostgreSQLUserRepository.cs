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
        public IUser GenerateRepositoryWithUsers()
        {
            var userCollection = TestRepositoryDataGenerator.GenerateTestUsers().Result;

            var repository = new PostgreSQLUserRepository(CreateInMemoryContext(userCollection));

            return repository;
        }

        public IUser GenerateRepositoryWithUser()
        {
            var userCollection = TestRepositoryDataGenerator.GenerateTestUser().Result;

            var repository = new PostgreSQLUserRepository(CreateInMemoryContext(userCollection));

            return repository;
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
            var repository = GenerateRepositoryWithUsers();

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
            var repository = GenerateRepositoryWithUsers();
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
            var repository = GenerateRepositoryWithUser();

            // Act
            var oldUsers = await repository.GetAll();
            var result = await repository.Delete(1);
            await repository.Save();
            var newUsers = await repository.GetAll();

            // Assert
            Assert.Single(oldUsers);
            Assert.Empty(newUsers);
            Assert.Equal(1, result);
        }
        #endregion

        #region FailedTests
        [Fact]
        public async Task UserRepositoryReturnsNull()
        {
            // Arrange
            var repository = GenerateRepositoryWithUsers();

            // Act
            var result = await repository.GetById(Int32.MaxValue);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Initialization
        private ShopichContext CreateInMemoryContext(User[] userCollection)
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

        private ShopichContext CreateInMemoryContext(User userCollection)
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