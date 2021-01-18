using Microsoft.AspNetCore.Mvc;
using Moq;
using Shopich.Business_logic;
using Shopich.Controllers;
using Shopich.Models;
using Shopich.Repositories.interfaces;
using Shopich.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ShopichTests.Tests.TestLogic
{
    public class TestAuthController
    {
        [Fact]
        public async void TestRegister()
        {
            // Arrange
            var mock = new Mock<IUser>();
            var sut = new AuthController(mock.Object);
            var user = new RegisterModel { name = "Zeynal", surname = "Zeynalov", email = "123@mail.ru", password = "12345678" };

            // Act
            var action = await sut.Register(user);

            // Assert
            Assert.IsType<CreatedAtActionResult>(action);
            mock.Verify(
                x => x.GetByEmail("123@mail.ru"),
                Times.Once
            );
        }

        [Fact]
        public async void TestLogin()
        {
            // Arrange
            var mock = new Mock<IUser>();
            var sut = new AuthController(mock.Object);

            // Act
            var action = sut.Login(new LoginModel { Email = "Sanginov", Password = "qwerty", RememberMe = false } );

            // Assert
            Assert.IsType<ObjectResult>(action);
            mock.Verify(
                x => x.Include(u => u.Role),
                Times.Once
            );
        }
    }
}
