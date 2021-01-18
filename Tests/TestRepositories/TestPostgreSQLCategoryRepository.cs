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

        public ICategory GenerateRepositoryWithCategories()
        {
            var categoryCollection = TestRepositoryDataGenerator.GenerateTestCategories().Result;

            var repository = new PostgreSQLCategoryRepository(CreateInMemoryContext(categoryCollection));
            return repository;
        }

        public ICategory GenerateRepositoryWithCategory()
        {
            var categoryCollection = TestRepositoryDataGenerator.GenerateTestCategory().Result;

            var repository = new PostgreSQLCategoryRepository(CreateInMemoryContext(categoryCollection));
            return repository;
        }
        #region SuccessTests
        [Fact]
        public async Task CategoryRepositoryReturnsCategories()
        {
            // Arrange
            var stub = new Mock<ICategory>();
            stub.Setup(repo => repo.GetAll()).Returns(TestRepositoryDataGenerator.GenerateTestCategories());
            var categoryRepository = stub.Object;

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
            var stub = new Mock<ICategory>();
            stub.Setup(repo => repo.GetById(3)).Returns(TestRepositoryDataGenerator.GenerateTestCategory());
            var categoryRepository = stub.Object;

            // Act
            var result = await categoryRepository.GetById(3);

            // Assert
            Assert.IsType<Category>(result);
            Assert.Equal("Fruits", result.CategoryName);
        }

        [Fact]
        public async Task CategoryRepositoryCreateCategory()
        {
            // Arrange
            var repository = GenerateRepositoryWithCategories();

            // Act
            await repository.Create(new CategoryBuilder().WithId(10).WithName("Sport").Build());
            await repository.Save();

            var result = await repository.GetAll();

            // Assert
            Assert.IsType<Category[]>(result);
            Assert.Equal(8, result.Length);
        }

        [Fact]
        public async Task CategoryRepositoryUpdateCategory()
        {
            // Arrange
            var repository = GenerateRepositoryWithCategories();
            var oldValue = await repository.GetById(1);
            oldValue.CategoryName = "Farm";

            // Act
            repository.Update(oldValue);
            await repository.Save();

            var result = await repository.GetById(1);

            // Assert
            Assert.IsType<Category>(result);
            Assert.Equal("Farm", result.CategoryName);
        }

        [Fact]
        public async Task CategoryRepositoryDeleteCategory()
        {
            // Arrange
            var repository = GenerateRepositoryWithCategory();

            // Act
            var oldCategories = await repository.GetAll();
            var result = await repository.Delete(1);
            await repository.Save();
            var newCategories = await repository.GetAll();

            // Assert
            Assert.Single(oldCategories);
            Assert.Empty(newCategories);
            Assert.Equal(1, result);
        }
        #endregion

        #region FailedTests
        [Fact]
        public async Task CategoryRepositoryReturnsNull()
        {
            // Arrange
            var repository = GenerateRepositoryWithCategories();

            // Act
            var result = await repository.GetById(Int32.MaxValue);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Initialization
        private ShopichContext CreateInMemoryContext(Category[] categoryCollection)
        {
            // Database' Name have to be unique in memory to prevent collisions
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShopichContext(options);

            context.AddRangeAsync(categoryCollection);
            context.SaveChanges();

            return context;
        }

        private ShopichContext CreateInMemoryContext(Category categoryCollection)
        {
            // Database' Name have to be unique in memory to prevent collisions
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShopichContext(options);

            context.AddRangeAsync(categoryCollection);
            context.SaveChanges();

            return context;
        }
        #endregion
    }
}