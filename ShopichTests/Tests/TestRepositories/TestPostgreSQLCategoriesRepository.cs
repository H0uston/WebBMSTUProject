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
    public class TestPostgreSQLCategoriesRepository
    {

        public ICategories GenerateRepositoryWithCategoriesCollection()
        {
            var categoryCollection = TestRepositoryDataGenerator.GenerateTestCategoriesCollection().Result;

            var repository = new PostgreSQLCategoriesRepository(CreateInMemoryContext(categoryCollection));
            return repository;
        }

        public ICategories GenerateRepositoryWithCategoriesSingle()
        {
            var categoryCollection = TestRepositoryDataGenerator.GenerateTestCategoriesSingle().Result;

            var repository = new PostgreSQLCategoriesRepository(CreateInMemoryContext(categoryCollection));
            return repository;
        }
        #region SuccessTests
        [Fact]
        public async Task CategoriesRepositoryReturnsCategories()
        {
            // Arrange
            var stub = new Mock<ICategories>();
            stub.Setup(repo => repo.GetAll()).Returns(TestRepositoryDataGenerator.GenerateTestCategoriesCollection());
            var categoryRepository = stub.Object;

            // Act
            var result = await categoryRepository.GetAll();

            // Assert
            Assert.IsType<Categories[]>(result);
            Assert.Equal(6, result.Length);
        }

        [Fact]
        public async Task CategoriesRepositoryReturnsCategoriesById()
        {
            // Arrange
            var stub = new Mock<ICategories>();
            stub.Setup(repo => repo.GetById(3)).Returns(TestRepositoryDataGenerator.GenerateTestCategoriesSingle());
            var categoryRepository = stub.Object;

            // Act
            var result = await categoryRepository.GetById(3);

            // Assert
            Assert.IsType<Categories>(result);
            Assert.Equal(3, result.ProductId);
        }

        [Fact]
        public async Task CategoriesRepositoryCreateCategories()
        {
            // Arrange
            var repository = GenerateRepositoryWithCategoriesCollection();

            // Act
            await repository.Create(new CategoriesBuilder().WithId(10).Build());
            await repository.Save();

            var result = await repository.GetAll();

            // Assert
            Assert.IsType<Categories[]>(result);
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public async Task CategoryRepositoryUpdateCategory()
        {
            // Arrange
            var repository = GenerateRepositoryWithCategoriesCollection();
            var oldValue = await repository.GetById(1);
            oldValue.ProductId = 3;

            // Act
            repository.Update(oldValue);
            await repository.Save();

            var result = await repository.GetById(1);

            // Assert
            Assert.IsType<Categories>(result);
            Assert.Equal(3, result.ProductId);
        }

        [Fact]
        public async Task CategoryRepositoryDeleteCategory()
        {
            // Arrange
            var repository = GenerateRepositoryWithCategoriesSingle();

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
            var repository = GenerateRepositoryWithCategoriesCollection();

            // Act
            var result = await repository.GetById(Int32.MaxValue);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region Initialization
        private ShopichContext CreateInMemoryContext(Categories[] categoryCollection)
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

        private ShopichContext CreateInMemoryContext(Categories categoryCollection)
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