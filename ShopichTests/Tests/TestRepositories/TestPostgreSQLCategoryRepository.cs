﻿using Microsoft.EntityFrameworkCore;
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
        private readonly Category[] categoryCollection;
        private readonly ICategory repository;

        public TestPostgreSQLCategoryRepository()
        {
            categoryCollection = TestRepositoryDataGenerator.GenerateTestCategories().Result;

            repository = new PostgreSQLCategoryRepository(CreateInMemoryContext());
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

            // Act
            await repository.Delete(1);
            await repository.Save();

            var result = await repository.GetById(1);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region FailedTests
        [Fact]
        public async Task CategoryRepositoryReturnsNull()
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

            context.AddRangeAsync(categoryCollection);
            context.SaveChanges();

            return context;
        }
        #endregion
    }
}