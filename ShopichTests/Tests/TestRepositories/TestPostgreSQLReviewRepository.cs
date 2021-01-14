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
        private readonly Review[] reviewCollection;
        private readonly IReview repository;

        public TestPostgreSQLReviewRepository()
        {
            reviewCollection = TestRepositoryDataGenerator.GenerateTestReviews().Result;

            repository = new PostgreSQLReviewRepository(CreateInMemoryContext());
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
        public async Task ReviewRepositoryReturnsNull()
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

            context.AddRangeAsync(reviewCollection);
            context.SaveChanges();

            return context;
        }
        #endregion
    }
}