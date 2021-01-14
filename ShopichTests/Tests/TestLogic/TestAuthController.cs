using Moq;
using Shopich.Controllers;
using Shopich.Models;
using Shopich.Repositories.interfaces;
using Shopich.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShopichTests.Tests.TestLogic
{
    public class TestAuthController
    {
        [Fact]
        public async void TestRegister()
        {
            var mock = new Mock<IUser>();
            var sut = new AuthController(mock.Object);

            await sut.Register(new RegisterModel { name = "Zeynal", surname = "Zeynalov", email = "123@mail.ru", password = "12345678" });

            mock.Verify(
                x => x.GetByEmail("123@mail.ru"),
                Times.Once
            );
        }
    }
}
