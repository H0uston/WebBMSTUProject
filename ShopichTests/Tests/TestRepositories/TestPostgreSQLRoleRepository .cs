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

        #region SuccessTests
        [Fact]
        public async Task RoleRepositoryReturnsRoles()
        {
            // Arrange
            var mock = new Mock<IRole>();
            mock.Setup(repo => repo.GetAll()).Returns(GenerateTestRoles());
            var roleRepository = mock.Object;

            // Act
            var result = await roleRepository.GetAll();

            // Assert
            Assert.IsType<Category[]>(result);
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public async Task RoleRepositoryReturnsRoleById()
        {
            // Arrange
            var mock = new Mock<IRole>();
            mock.Setup(repo => repo.GetById(3)).Returns(GenerateTestRole());
            var roleRepository = mock.Object;

            // Act
            var result = await roleRepository.GetById(3);

            // Assert
            Assert.IsType<Category>(result);
            Assert.Equal("Fruits", result.RoleName);
        }
        #endregion

        #region FailedTests
        [Fact]
        public async Task RoleRepositoryReturnsWrongCountOfRoles()
        {
            // Arrange
            var mock = new Mock<IRole>();
            mock.Setup(repo => repo.GetAll()).Returns(GenerateTestRoles());
            var categoryRepository = mock.Object;

            // Act
            var result = await categoryRepository.GetAll();

            // Assert
            Assert.IsType<Role[]>(result);
            Assert.NotEqual(6, result.Length);
            Assert.NotEqual(8, result.Length);
        }
        #endregion

        #region Initialization
        private async Task<Role[]> GenerateTestRoles()
        {
            var rolesCollection = new Role[]
            {
                
            };
            return rolesCollection;
        }

        private async Task<Role> GenerateTestRole()
        {
            var role = new RoleBuilder().Build();

            return role;
        }
        #endregion
    }
}