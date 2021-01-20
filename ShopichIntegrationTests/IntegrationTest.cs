using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Shopich;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Shopich.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Shopich.ViewModels;
using Nancy.Json;
using Newtonsoft.Json;
using System.Linq;
using ShopichTests.DataBuilders;

namespace ShopichIntegrationTests
{
    
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected readonly WebApplicationFactory<Startup> appFactory;

        protected IntegrationTest()
        {
            appFactory = new WebApplicationFactory<Startup>()
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
                InitializeDbForTests(context);
            }
        }

        public static void InitializeDbForTests(ShopichContext db)
        {
            db.Roles.AddRange(TestDataGenerator.GetRoles());
            db.Users.Add(TestDataGenerator.GetCurrentUser());
            db.SaveChanges();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var loginResponse = await TestClient.PostAsJsonAsync("api/v1/auth/login", new LoginBuilder()
                .WithEmail("test@integration.com")
                .WithPassword("SomePass1234!").Build());

            var loginResult = await loginResponse.Content.ReadAsStringAsync();

            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(loginResult);
            var token = values["access_token"];

            return token;
        }
    }
}
