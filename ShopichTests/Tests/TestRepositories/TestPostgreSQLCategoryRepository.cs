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
    public class TestPostgreSQLCategoryRepository
    {

        #region SuccessTests
        [Fact]
        public async Task CategoryRepositoryReturnsCategories()
        {
            // Arrange
            var mock = new Mock<ICategory>();
            mock.Setup(repo => repo.GetAll()).Returns(GenerateTestCategories());
            var categoryRepository = mock.Object;

            // Act
            var result = await categoryRepository.GetAll();

            // Assert
            Assert.IsType<Category[]>(result);
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public async Task CategoryRepositoryReturnsCategoryById()
        {
            // Arrange
            var mock = new Mock<ICategory>();
            mock.Setup(repo => repo.GetById(3)).Returns(GenerateTestCategory());
            var categoryRepository = mock.Object;

            // Act
            var result = await categoryRepository.GetById(3);

            // Assert
            Assert.IsType<Category>(result);
            Assert.Equal("Fruits", result.CategoryName);
        }
        #endregion

        #region FailedTests
        [Fact]
        public async Task CategoryRepositoryReturnsWrongCountOfCategories()
        {
            // Arrange
            var mock = new Mock<ICategory>();
            mock.Setup(repo => repo.GetAll()).Returns(GenerateTestCategories());
            var categoryRepository = mock.Object;

            // Act
            var result = await categoryRepository.GetAll();

            // Assert
            Assert.IsType<Category[]>(result);
            Assert.NotEqual(6, result.Length);
            Assert.NotEqual(8, result.Length);
        }
        #endregion

        #region Initialization
        private async Task<Category[]> GenerateTestCategories()
        {
            var categoryCollection = new Category[]
            {
                new CategoryBuilder().WithId(1).WithName("Fruits").Build(),
                new CategoryBuilder().WithId(2).WithName("Vegetables").Build(),
                new CategoryBuilder().WithId(3).WithName("Apples").Build(),
                new CategoryBuilder().WithId(4).WithName("Cucumbers").Build(),
                new CategoryBuilder().WithId(5).WithName("Meat").Build(),
                new CategoryBuilder().WithId(6).WithName("Fish").Build(),
                new CategoryBuilder().WithId(7).WithName("Dairy").Build(),
            };
            return categoryCollection;
        }

        private async Task<Category> GenerateTestCategory()
        {
            var category = new CategoryBuilder().WithId(3).WithName("Fruits").Build();

            return category;
        }
        #endregion
    }
}