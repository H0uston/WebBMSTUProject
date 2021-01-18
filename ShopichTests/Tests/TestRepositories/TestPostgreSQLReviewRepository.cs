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
    public class TestPostgreSQLReviewRepository
    {
        public IReview GenerateRepositoryWithReviews()
        {
            var reviewCollection = TestRepositoryDataGenerator.GenerateTestReviews().Result;

            var repository = new PostgreSQLReviewRepository(CreateInMemoryContext(reviewCollection));

            return repository;
        }

        public IReview GenerateRepositoryWithReview()
        {
            var reviewCollection = TestRepositoryDataGenerator.GenerateTestReview().Result;

            var repository = new PostgreSQLReviewRepository(CreateInMemoryContext(reviewCollection));

            return repository;
        }

        #region SuccessTests
        [Fact]
        public async Task ReviewRepositoryReturnsReviews()
        {
            // Arrange
            var stub = new Mock<IReview>();
            stub.Setup(repo => repo.GetAll()).Returns(TestRepositoryDataGenerator.GenerateTestReviews());
            var reviewRepository = stub.Object;

            // Act
            var result = await reviewRepository.GetAll();

            // Assert
            Assert.IsType<Review[]>(result);
            Assert.Equal(6, result.Length);
        }

        [Fact]
        public async Task ReviewRepositoryReturnsReviewById()
        {
            // Arrange
            var stub = new Mock<IReview>();
            stub.Setup(repo => repo.GetById(3)).Returns(TestRepositoryDataGenerator.GenerateTestReview());
            var reviewRepository = stub.Object;

            // Act
            var result = await reviewRepository.GetById(3);

            // Assert
            Assert.IsType<Review>(result);
            Assert.Equal("Good", result.ReviewText);
        }

        [Fact]
        public async Task ReviewRepositoryCreateReview()
        {
            // Arrange
            var repository = GenerateRepositoryWithReviews();

            // Act
            await repository.Create(new ReviewBuilder().WithId(10).WithText("WOW").Build());
            await repository.Save();

            var result = await repository.GetAll();

            // Assert
            Assert.IsType<Review[]>(result);
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public async Task ReviewRepositoryUpdateReview()
        {
            // Arrange
            var repository = GenerateRepositoryWithReviews();
            var oldValue = await repository.GetById(1);
            oldValue.ReviewText = "Bad";

            // Act
            repository.Update(oldValue);
            await repository.Save();

            var result = await repository.GetById(1);

            // Assert
            Assert.IsType<Review>(result);
            Assert.Equal("Bad", result.ReviewText);
        }

        [Fact]
        public async Task ReviewRepositoryDeleteReview()
        {
            // Arrange
            var repository = GenerateRepositoryWithReview();

            // Act
            var oldReviews = await repository.GetAll();
            var result = await repository.Delete(1);
            await repository.Save();
            var newReviews = await repository.GetAll();

            // Assert
            Assert.Single(oldReviews);
            Assert.Empty(newReviews);
            Assert.Equal(1, result);
        }
        #endregion

        #region FailedTests
        [Fact]
        public async Task ReviewRepositoryReturnsNull()
        {
            // Arrange
            var repository = GenerateRepositoryWithReviews();

            // Act
            var result = await repository.GetById(Int32.MaxValue);

            // Assert
            Assert.Null(result);
        }
        #endregion


        #region Initialization
        private ShopichContext CreateInMemoryContext(Review[] reviewCollection)
        {
            // Database' Name have to be unique in memory to prevent collisions
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShopichContext(options);

            context.AddRangeAsync(reviewCollection);
            context.SaveChanges();

            return context;
        }

        private ShopichContext CreateInMemoryContext(Review reviewCollection)
        {
            // Database' Name have to be unique in memory to prevent collisions
            var options = new DbContextOptionsBuilder<ShopichContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ShopichContext(options);

            context.AddRangeAsync(reviewCollection);
            context.SaveChanges();

            return context;
        }
        #endregion
    }
}