using Moq;
using Shopich.Controllers.api;
using Shopich.Models;
using Shopich.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShopichTests.Tests.TestLogic
{
    public class TestCategoryController
    {
        [Fact]
        public async void TestCategoryControllerGetAll()
        {
            // Arrange
            var mock = new Mock<ICategory>();
            var sut = new CategoryController(mock.Object);

            // Act
            var result = await sut.GetAll();

            // Assert
            Assert.IsType<Category[]>(result);
            mock.Verify(
                x => x.GetAll(),
                Times.Once
            );
        }

        [Fact]
        public async void TestCategoryControllerGetById()
        {
            // Arrange
            var mock = new Mock<ICategory>();
            var sut = new CategoryController(mock.Object);

            // Act
            var result = await sut.GetCategory(1);

            // Assert
            mock.Verify(
                x => x.GetById(1),
                Times.Once
            );
        }
    }
}
