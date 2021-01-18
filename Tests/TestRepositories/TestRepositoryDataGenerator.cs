using Shopich.Models;
using ShopichTests.DataBuilders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopichTests.Tests.TestRepositories
{
    public class TestRepositoryDataGenerator
    {
        public static async Task<Category[]> GenerateTestCategories()
        {
            var categoryCollection = new Category[]
            {
                new CategoryBuilder().WithId(1).WithName("Fruits").Build(),
                new CategoryBuilder().WithId(2).WithName("Vegetables").Build(),
                new CategoryBuilder().WithId(3).WithName("Apples").Build(),
                new CategoryBuilder().WithId(4).WithName("Cucumbers").Build(),
                new CategoryBuilder().WithId(5).WithName("Meat").Build(),
                new CategoryBuilder().WithId(6).WithName("Fish").Build(),
                new CategoryBuilder().WithId(7).WithName("Dairy").Build(),
            };
            return categoryCollection;
        }

        public static async Task<Category> GenerateTestCategory()
        {
            var category = new CategoryBuilder().WithId(1).WithName("Fruits").Build();

            return category;
        }

        public static async Task<Order[]> GenerateTestOrders()
        {
            var orderCollection = new Order[]
            {
                new OrderBuilder().WithId(1).WithUserid(1).WithDate("2020-01-02").WithIsApproved(false).Build(),
                new OrderBuilder().WithId(2).WithUserid(1).WithDate("2020-01-01").WithIsApproved(true).Build(),
                new OrderBuilder().WithId(3).WithUserid(2).WithDate("2020-01-01").WithIsApproved(true).Build(),
                new OrderBuilder().WithId(4).WithUserid(3).WithDate("2020-01-03").WithIsApproved(false).Build(),
                new OrderBuilder().WithId(5).WithUserid(3).WithDate("2020-01-02").WithIsApproved(true).Build(),
                new OrderBuilder().WithId(6).WithUserid(3).WithDate("2020-01-01").WithIsApproved(true).Build(),
            };

            return orderCollection;
        }

        public static async Task<Order> GenerateTestOrder()
        {
            var order = new OrderBuilder().WithId(1).WithUserid(1).WithDate("2020-01-02").WithIsApproved(false).Build();

            return order;
        }

        public static async Task<Product[]> GenerateTestProducts()
        {
            var productCollection = new Product[]
            {
                new ProductBuilder().WithId(1).WithName("Potato").WithPrice(11).WithDiscount(1).WithAvailability(true).Build(),
                new ProductBuilder().WithId(2).WithName("Bread").WithPrice(22).WithDiscount(0).WithAvailability(true).Build(),
                new ProductBuilder().WithId(3).WithName("Beans").WithPrice(33).WithDiscount(0).WithAvailability(false).Build(),
                new ProductBuilder().WithId(4).WithName("Eggs").WithPrice(44).WithDiscount(7).WithAvailability(true).Build(),
                new ProductBuilder().WithId(5).WithName("Milk").WithPrice(55).WithDiscount(0).WithAvailability(false).Build(),
                new ProductBuilder().WithId(6).WithName("Juice").WithPrice(66).WithDiscount(0).WithAvailability(true).Build(),
            };

            return productCollection;
        }

        public static async Task<Product> GenerateTestProduct()
        {
            var product = new ProductBuilder().WithId(1).WithName("Potato").WithPrice(11).WithDiscount(1).WithAvailability(true).Build();

            return product;
        }

        public static async Task<Review[]> GenerateTestReviews()
        {
            var reviewCollection = new Review[]
            {
                new ReviewBuilder().WithId(1).WithUserid(1).WithProductId(1).WithText("Good").WithDate("2020-01-01").WithRating(3).Build(),
                new ReviewBuilder().WithId(2).WithUserid(1).WithProductId(2).WithText("I like it").WithDate("2020-01-02").WithRating(4).Build(),
                new ReviewBuilder().WithId(3).WithUserid(1).WithProductId(3).WithText("Awesome").WithDate("2020-01-03").WithRating(5).Build(),
                new ReviewBuilder().WithId(4).WithUserid(2).WithProductId(4).WithText("Bad").WithDate("2020-01-03").WithRating(2).Build(),
                new ReviewBuilder().WithId(5).WithUserid(3).WithProductId(5).WithText("Very bad").WithDate("2020-01-04").WithRating(1).Build(),
                new ReviewBuilder().WithId(6).WithUserid(4).WithProductId(6).WithText("Great").WithDate("2020-01-05").WithRating(5).Build(),
            };

            return reviewCollection;
        }

        public static async Task<Review> GenerateTestReview()
        {
            var review = new ReviewBuilder().WithId(1).WithUserid(1).WithProductId(1).WithText("Good").WithDate("2020-01-01").WithRating(3).Build();

            return review;
        }

        public static async Task<Role[]> GenerateTestRoles()
        {
            var rolesCollection = new Role[]
            {
                new RoleBuilder().WithId(1).WithName("Admin").Build(),
                new RoleBuilder().WithId(2).WithName("Moderator").Build(),
                new RoleBuilder().WithId(3).WithName("User").Build(),
            };
            return rolesCollection;
        }

        public static async Task<Role> GenerateTestRole()
        {
            var role = new RoleBuilder().WithId(1).WithName("User").Build();

            return role;
        }

        public static async Task<User[]> GenerateTestUsers()
        {
            var userCollection = new User[]
            {
                new UserBuilder().WithId(1).WithEmail("a@mail.ru").WithPassword("a").WithPhone("88005553535").WithName("Peter")
                    .WithSurname("Vdovin").WithCity("Moscow").WithStreet("Lenina").WithHouse("22").WithFlat("8").WithNoRoleId().Build(),
                new UserBuilder().WithId(2).WithEmail("b@mail.ru").WithPassword("b").WithPhone("88001234567").WithName("Zeynal")
                    .WithSurname("Zeynalov").WithCity("Derbent").WithStreet("Gagarina").WithHouse("67").WithFlat("1")
                    .WithIndex(88041).WithBirthdate("2000-03-25").WithRoleId(2).Build(),
                new UserBuilder().WithId(3).WithEmail("c@mail.ru").WithPassword("c").WithPhone("88002345678").WithName("Azamat")
                    .WithSurname("Sanginov").WithCity("Asia").WithStreet("Hromosom").WithHouse("47").WithFlat("1337")
                    .WithIndex(10101).WithBirthdate("2010-04-01").WithRoleId(2).Build(),
                new UserBuilder().WithId(4).WithEmail("d@mail.ru").WithPassword("d").WithPhone("88003456789").WithName("George")
                    .WithSurname("Kornilaev").WithCity("Is-Swieqi").WithStreet("Triq Il-Terz").WithHouse("89").WithFlat("135")
                    .WithIndex(80808).WithBirthdate("1999-11-12").WithRoleId(3).Build(),
                new UserBuilder().WithId(5).WithEmail("e@mail.ru").WithPassword("e").WithPhone("88004567890").WithName("Eugenie")
                    .WithSurname("Ulasik").WithCity("Prague").WithStreet("Lasky").WithHouse("14").WithFlat("88").Build(),
                new UserBuilder().WithId(6).WithEmail("famine@mail.ru").WithPassword("afaw").WithPhone("88004412523").WithName("Andrew")
                    .WithSurname("Poopkin").WithCity("Santiago").WithStreet("St. Pedro").WithHouse("12").WithFlat("5").Build(),
            };
            return userCollection;
        }

        public static async Task<User> GenerateTestUser()
        {
            var user = new UserBuilder().WithId(1).WithEmail("a@mail.ru").WithPassword("a").WithPhone("88005553535").WithName("Peter")
                    .WithSurname("Vdovin").WithCity("Moscow").WithStreet("Lenina").WithHouse("22").WithFlat("8").WithNoRoleId().Build();

            return user;
        }

        public static async Task<Orders[]> GenerateTestOrdersCollection()
        {
            var ordersCollection = new Orders[]
            {
            new OrdersBuilder().WithId(1).WithOrderId(1).WithProductId(1).WithCount(10).Build(),
            new OrdersBuilder().WithId(2).WithOrderId(1).WithProductId(2).WithCount(20).Build(),
            new OrdersBuilder().WithId(3).WithOrderId(2).WithProductId(3).WithCount(30).Build(),
            new OrdersBuilder().WithId(4).WithOrderId(3).WithProductId(4).WithCount(40).Build(),
            new OrdersBuilder().WithId(5).WithOrderId(3).WithProductId(6).WithCount(50).Build(),
            new OrdersBuilder().WithId(6).WithOrderId(3).WithProductId(6).WithCount(60).Build(),
            };

            return ordersCollection;
        }

        public static async Task<Orders> GenerateTestOrdersSingle()
        {
            var orders = new OrdersBuilder().WithId(1).WithOrderId(1).WithProductId(1).WithCount(10).Build();

            return orders;
        }

        public static async Task<Categories[]> GenerateTestCategoriesCollection()
        {
            var categoriesCollection = new Categories[]
            {
                new CategoriesBuilder().WithId(1).WithCategoryId(1).WithProductId(1).Build(),
                new CategoriesBuilder().WithId(2).WithCategoryId(1).WithProductId(2).Build(),
                new CategoriesBuilder().WithId(3).WithCategoryId(1).WithProductId(3).Build(),
                new CategoriesBuilder().WithId(4).WithCategoryId(2).WithProductId(4).Build(),
                new CategoriesBuilder().WithId(5).WithCategoryId(3).WithProductId(5).Build(),
                new CategoriesBuilder().WithId(6).WithCategoryId(3).WithProductId(6).Build(),
            };

            return categoriesCollection;
        }

        public static async Task<Categories> GenerateTestCategoriesSingle()
        {
            var categories = new CategoriesBuilder().WithId(1).WithCategoryId(1).WithProductId(3).Build();

            return categories;
        }
    }
}
