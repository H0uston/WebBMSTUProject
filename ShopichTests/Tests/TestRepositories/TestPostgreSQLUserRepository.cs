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

        #region SuccessTests
        [Fact]
        public async Task CategoryRepositoryReturnsUsers()
        {
            // Arrange
            var mock = new Mock<IUser>();
            mock.Setup(repo => repo.GetAll()).Returns(GenerateTestUsers());
            var userRepository = mock.Object;

            // Act
            var result = await userRepository.GetAll();

            // Assert
            Assert.IsType<User[]>(result);
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public async Task UserRepositoryReturnsUserById()
        {
            // Arrange
            var mock = new Mock<IUser>();
            mock.Setup(repo => repo.GetById(3)).Returns(GenerateTestUser());
            var userRepository = mock.Object;

            // Act
            var result = await userRepository.GetById(3);

            // Assert
            Assert.IsType<Category>(result);
            Assert.Equal("Fruits", result.UserName);
        }
        #endregion

        #region FailedTests
        [Fact]
        public async Task UserRepositoryReturnsWrongCountOfUsers()
        {
            // Arrange
            var mock = new Mock<IUser>();
            mock.Setup(repo => repo.GetAll()).Returns(GenerateTestUsers());
            var userRepository = mock.Object;

            // Act
            var result = await userRepository.GetAll();

            // Assert
            Assert.IsType<User[]>(result);
            Assert.NotEqual(6, result.Length);
            Assert.NotEqual(8, result.Length);
        }
        #endregion

        #region Initialization
        private async Task<User[]> GenerateTestUsers()
        {
            var userCollection = new User[]
            {
            };
            return userCollection;
        }

        private async Task<User> GenerateTestUser()
        {
            var user = new UserBuilder().Build();

            return user;
        }
        #endregion
    }
}