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
    public class TestPostgreSQLRoleRepository
    {
        private readonly Role[] roleCollection;
        private readonly IRole repository;

        public TestPostgreSQLRoleRepository()
        {
            roleCollection = TestRepositoryDataGenerator.GenerateTestRoles().Result;

            repository = new PostgreSQLRoleRepository(CreateInMemoryContext());
        }

        #region SuccessTests
        [Fact]
        public async Task RoleRepositoryReturnsRoles()
        {
            // Arrange
            var stub = new Mock<IRole>();
            stub.Setup(repo => repo.GetAll()).Returns(TestRepositoryDataGenerator.GenerateTestRoles());
            var roleRepository = stub.Object;

            // Act
            var result = await roleRepository.GetAll();

            // Assert
            Assert.IsType<Role[]>(result);
            Assert.Equal(3, result.Length);
        }

        [Fact]
        public async Task RoleRepositoryReturnsRoleById()
        {
            // Arrange
            var stub = new Mock<IRole>();
            stub.Setup(repo => repo.GetById(3)).Returns(TestRepositoryDataGenerator.GenerateTestRole());
            var roleRepository = stub.Object;

            // Act
            var result = await roleRepository.GetById(3);

            // Assert
            Assert.IsType<Role>(result);
            Assert.Equal("User", result.RoleName);
        }

        [Fact]
        public async Task RoleRepositoryCreateRole()
        {
            // Arrange

            // Act
            await repository.Create(new RoleBuilder().WithId(4).WithName("Anonim").Build());
            await repository.Save();

            var result = await repository.GetAll();

            // Assert
            Assert.IsType<Role[]>(result);
            Assert.Equal(4, result.Length);
        }

        [Fact]
        public async Task RoleRepositoryUpdateRole()
        {
            // Arrange
            var oldValue = await repository.GetById(3);
            oldValue.RoleName = "Observer";

            // Act
            repository.Update(oldValue);
            await repository.Save();

            var result = await repository.GetById(3);

            // Assert
            Assert.IsType<Role>(result);
            Assert.Equal("Observer", result.RoleName);
        }

        [Fact]
        public async Task RoleRepositoryDeleteRole()
        {
            // Arrange

            // Act
            await repository.Delete(4);
            await repository.Save();

            var result = await repository.GetById(4);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region FailedTests
        [Fact]
        public async Task RoleRepositoryReturnsNull()
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

            context.AddRangeAsync(roleCollection);
            context.SaveChanges();

            return context;
        }
        #endregion
    }
}