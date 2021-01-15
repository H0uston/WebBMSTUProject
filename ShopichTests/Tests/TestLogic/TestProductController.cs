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
    public class TestProductController
    {
        [Fact]
        public async void TestProductControllerGetAll()
        {
            // Arrange
            var mock = new Mock<IProduct>();
            var sut = new ProductController(mock.Object);

            // Act
            var result = await sut.GetAll();

            // Assert
            mock.Verify(
                x => x.GetAll(),
                Times.Once
            );
        }

        [Fact]
        public async void TestProductControllerGetById()
        {
            // Arrange
            var mock = new Mock<IProduct>();
            var sut = new ProductController(mock.Object);

            // Act
            var result = await sut.GetProduct(1);

            // Assert
            mock.Verify(
                x => x.GetById(1),
                Times.Once
            );
        }
    }
}
