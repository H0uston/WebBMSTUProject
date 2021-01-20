using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shopich;
using Shopich.Models;
using ShopichEndToEndTests;
using ShopichTests.DataBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ShopichE2ETests.Tests
{
    [Binding]
    public sealed class OpenProductPageSteps
    {
        protected HttpClient TestClient;

        [Given("@register attributes")]
        public async Task RegisterAttributes()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services => {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                typeof(DbContextOptions<ShopichContext>));
                        services.Remove(descriptor);
                        services.AddDbContext<ShopichContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });
            TestClient = appFactory.CreateClient();

            using (var scope = appFactory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ShopichContext>();
                context.Roles.AddRange(await TestDataGenerator.GenerateTestRoles());
                context.Users.AddRange(await TestDataGenerator.GenerateTestUsers());
                context.Products.AddRange(await TestDataGenerator.GenerateTestProducts());
                context.Categories.AddRange(await TestDataGenerator.GenerateTestCategories());
                context.CategoryCollection.AddRange(await TestDataGenerator.GenerateTestCategoriesCollection());
                context.OrderCollection.AddRange(await TestDataGenerator.GenerateTestOrders());
                context.Reviews.AddRange(await TestDataGenerator.GenerateTestReviews());
                context.Orders.AddRange(await TestDataGenerator.GenerateTestOrdersCollection());
                context.SaveChanges();
            }

            var newUser = new RegisterBuilder().WithEmail("test@test.ru").WithPassword("12345").Build();
            var loginInfo = new LoginBuilder().WithEmail("test@test.ru").WithPassword("12345").Build();
            var newUserInString = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
            var loginInfoInString = new StringContent(JsonConvert.SerializeObject(loginInfo), Encoding.UTF8, "application/json");

            ScenarioContext.Current.Add("RegisterModel", newUserInString);
            ScenarioContext.Current.Add("LoginModel", loginInfoInString);
        }

        [When(@"I register")]
        public async Task WhenIRegister()
        {
            var registerString = (StringContent)ScenarioContext.Current["RegisterModel"];

            var registerResponse = await TestClient.PostAsync("api/v1/auth/register", registerString);
            var registerResult = await registerResponse.Content.ReadAsAsync<User>();

            ScenarioContext.Current.Add("registerResponse", registerResponse);
            ScenarioContext.Current.Add("registerResult", registerResult);
        }

        [Then(@"Registered Email should be correct")]
        public void ThenRegisteredEmailShouldBeCorrect()
        {
            var registerResponse = (HttpResponseMessage)ScenarioContext.Current["registerResponse"];
            var registerResult = (User)ScenarioContext.Current["registerResult"];

            registerResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            registerResult.UserEmail.Should().Be("test@test.ru");
        }

        [When(@"I login")]
        public async void WhenILogin()
        {
            var loginInfoInString = (StringContent)ScenarioContext.Current["LoginModel"];

            var loginResponse = await TestClient.PostAsync("api/v1/auth/login", loginInfoInString);
            var loginResult = await loginResponse.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(loginResult);
            var token = values["access_token"];
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            ScenarioContext.Current.Add("loginResponse", loginResponse);
        }

        [Then(@"Status should be ok")]
        public void ThenStatusShouldBeOk()
        {
            var loginResponse = (HttpResponseMessage)ScenarioContext.Current["loginResponse"];

            loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [When(@"I get on main page")]
        public async void WhenIGetOnMainPage()
        {
            var productsResponse = await TestClient.GetAsync("api/v1/products");
            var productsResult = await productsResponse.Content.ReadAsAsync<Product[]>();

            ScenarioContext.Current.Add("productsResponse", productsResponse);
            ScenarioContext.Current.Add("productsResult", productsResult);
        }

        [Then(@"I see all the products")]
        public void ThenISeeAllTheProducts()
        {
            var productsResult = (Product[])ScenarioContext.Current["productsResult"];
            var productsResponse = (HttpResponseMessage)ScenarioContext.Current["productsResponse"];

            productsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            productsResult.Length.Should().Be(6);
        }

        [When(@"I open about page of a product")]
        public async void WhenIOpenAboutPageOfAProduct()
        {
            var productResponse = await TestClient.GetAsync("api/v1/products/3");
            var productResult = await productResponse.Content.ReadAsAsync<Product>();

            var reviewsResponse = await TestClient.GetAsync("api/v1/products/3/reviews");
            var reviewResult = await reviewsResponse.Content.ReadAsAsync<Review[]>();

            ScenarioContext.Current.Add("productResponse", productResponse);
            ScenarioContext.Current.Add("productResult", productResult);

            ScenarioContext.Current.Add("reviewsResponse", reviewsResponse);
            ScenarioContext.Current.Add("reviewResult", reviewResult);
        }
        
        [Then(@"I see productInfo and reviews for this product")]
        public void ThenISeeProductInfoAndReviewsForThisProduct()
        {
            var productResult = (Product)ScenarioContext.Current["productResult"];
            var productResponse = (HttpResponseMessage)ScenarioContext.Current["productResponse"];

            var reviewResult = (Review[])ScenarioContext.Current["reviewsResult"];
            var reviewsResponse = (HttpResponseMessage)ScenarioContext.Current["reviewsResponse"];

            productResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            productResult.ProductId.Should().Be(3);
            productResult.ProductName.Should().Be("Beans");

            reviewsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            reviewResult.Length.Should().Be(1);
            reviewResult[0].ReviewText.Should().Be("Awesome");
        }
    }
}
