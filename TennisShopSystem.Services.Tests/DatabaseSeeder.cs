namespace TennisShopSystem.Services.Tests
{
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.Data;
    using System;

    public static class DatabaseSeeder
    {
        public static ApplicationUser SellerUser;
        public static ApplicationUser SellerUserTwo;
        public static ApplicationUser RegisteredUser;
        public static Seller Seller;
        public static Seller SellerTwo;
        public static Product Product;
        public static Product ProductTwo;
        public static Product ProductThree;
        public static Brand Brand;
        public static Brand BrandTwo;
        public static Category Category;
        public static Category CategoryTwo;
        public static Order Order;
        public static Order OrderTwo;
        public static OrderDetails OrderDetails;
        public static OrderDetails OrderDetailsTwo;
        public static List<OrderedItem> OrderedItems;
        public static List<OrderedItem> OrderedItemsTwo;

        public static void SeedDatabase(TennisShopDbContext dbContext)
        {
            SellerUser = new ApplicationUser
            {
                UserName = "Pesho",
                NormalizedUserName = "PESHO",
                Email = "peshoAgent@agenti.com",
                NormalizedEmail = "PESHOAGENT@AGENTI.COM",
                EmailConfirmed = false,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                SecurityStamp = "f9365731-d3b2-4b57-92b7-2916dfefc7fa",
                ConcurrencyStamp = "08a10f1b-21c9-4291-9174-2cc3335029fe",
                TwoFactorEnabled = false
            };

            SellerUserTwo = new ApplicationUser
            {
                UserName = "Sasho",
                NormalizedUserName = "SASHO",
                Email = "sashoAgent@agenti.com",
                NormalizedEmail = "SASHOAGENT@AGENTI.COM",
                EmailConfirmed = false,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                SecurityStamp = "f9365731-d3b2-4b57-92b7-2916dfefc7fa",
                ConcurrencyStamp = "08a10f1b-21c9-4291-9174-2cc3335029fe",
                TwoFactorEnabled = false
            };

            RegisteredUser = new ApplicationUser
            {
                UserName = "Gosho",
                NormalizedUserName = "GOSHO",
                Email = "goshouser@users.com",
                NormalizedEmail = "GOSHOUSER@USERS.COM",
                EmailConfirmed = false,
                PasswordHash = "481f6cc0511143ccdd7e2d1b1b94faf0a700a8b49cd13922a70b5ae28acaa8c5",
                SecurityStamp = "3ad4cb7b-c45f-425e-bd00-fd9c781a6ad7",
                ConcurrencyStamp = "dca963f3-3254-4e70-981a-e88b58382510",
                TwoFactorEnabled = false
            };

            Seller = new Seller
            {
                PhoneNumber = "+359877665544",
                User = SellerUser
            };

            SellerTwo = new Seller
            {
                PhoneNumber = "+359877665543",
                User = SellerUserTwo
            };

            Product = new Product
            {
                Id = Guid.Parse("e1ec4bb2-d890-40e7-bf4c-0ea73990c496"),
                Title = "Head Tennis Racket",
                Description = "This is the Prestige Head Tennis Racket! You will have a 200 km serve with it!",
                ImageUrl =
                    "https://cdn.shopify.com/s/files/1/0602/4763/8188/products/prestige-tour_1_768x.jpg?v=1691793838",
                Price = 140.00m,
                CreatedOn = default,
                IsAvailable = false,
                AvailableQuantity = 0,
                CategoryId = 1,
                BrandId = 2,
                SellerId = Seller.Id
            };

            ProductTwo = new Product
            {
                Id = Guid.Parse("0a5f0d11-c157-4ce0-a552-1f8bb032cee2"),
                Title = "Adidas Headband",
                Description = "This is an authentic headband for tennis! You will not sweat your eyes with it!",
                ImageUrl =
                    "https://i5.walmartimages.com/seo/Adidas-Tennis-Tie-Back-Aeroready-Reversible-Headband-Red-Black_2976cee6-649c-4be5-bc9c-5104efe73a01.120a9790acefba7ae71b7e4ef02a679a.jpeg?odnHeight=640&odnWidth=640&odnBg=FFFFFF",
                Price = 20.00m,
                CreatedOn = default,
                IsAvailable = false,
                AvailableQuantity = 0,
                CategoryId = 2,
                BrandId = 6,
                SellerId = SellerTwo.Id
            };

            ProductThree = new Product
            {
                Id = Guid.Parse("8ce39907-3ca4-44fd-955b-c30ccdeb67b8"),
                Title = "Adidas Headband",
                Description = "This is an authentic headband for tennis! You will not sweat your eyes with it!",
                ImageUrl =
                    "https://i5.walmartimages.com/seo/Adidas-Tennis-Tie-Back-Aeroready-Reversible-Headband-Red-Black_2976cee6-649c-4be5-bc9c-5104efe73a01.120a9790acefba7ae71b7e4ef02a679a.jpeg?odnHeight=640&odnWidth=640&odnBg=FFFFFF",
                Price = 20.00m,
                CreatedOn = default,
                IsAvailable = false,
                AvailableQuantity = 0,
                CategoryId = 2,
                BrandId = 6,
                SellerId = Seller.Id
            };

            Brand = new Brand
            {
                Id = 15,
                Name = "Nike",
            };

            BrandTwo = new Brand
            {
                Id = 16,
                Name = "Adidas"
            };

            Category = new Category
            {
                Id = 20,
                Name = "Rackets"
            };

            CategoryTwo = new Category
            {
                Id = 21,
                Name = "Balls"
            };

            OrderDetails = new OrderDetails
            {
                Id = Guid.Parse("085d317d-389b-48b2-8d82-dfb3d3de632a"),
                FirstName = "Rafael",
                LastName = "Nadal",
                Address = "Mallorca, Spain",
                PhoneNumber = "+359876565234",
                EmailAddress = "rafaelnadal@abv.bg",
                Comment = "Rafa",
                TotalPrice = 200,
                OrderedOn = null,
                OrderedItems = new List<OrderedItem>()
            };

            OrderDetailsTwo = new OrderDetails
            {
                Id = Guid.Parse("0ac77787-c4d5-412c-b9df-d3c2a76b4e2d"),
                FirstName = "Rafael",
                LastName = "Nadal",
                Address = "Mallorca, Spain",
                PhoneNumber = "+359876565234",
                EmailAddress = "rafaelnadal@abv.bg",
                Comment = "Rafa",
                TotalPrice = 100,
                OrderedOn = null,
                OrderedItems = new List<OrderedItem>()
            };

            OrderedItems = new List<OrderedItem>
            {
                new OrderedItem
                {
                    Id = Guid.Parse("42dac2e9-aca6-47bc-ac79-7f6d0a4b6a36"),
                    ProductId = Product.Id.ToString(),
                    OrderedQuantity = 1,
                    OrderDetailsId = OrderDetails.Id
                },
                new OrderedItem
                {
                    Id = Guid.Parse("13557a50-7d95-401d-bb7e-7948cc818ca0"),
                    ProductId = ProductTwo.Id.ToString(),
                    OrderedQuantity = 1,
                    OrderDetailsId = OrderDetails.Id
                }
            };

            OrderedItemsTwo = new List<OrderedItem>
            {
                new OrderedItem
                {
                    Id = Guid.Parse("3a387da9-bacb-4a58-8a5d-bf4d5d9b911d"),
                    ProductId = ProductThree.Id.ToString(),
                    OrderedQuantity = 1,
                    OrderDetailsId = OrderDetailsTwo.Id
                }
            };

            OrderDetails.OrderedItems = OrderedItems;
            OrderDetailsTwo.OrderedItems = OrderedItemsTwo;

            Order = new Order
            {
                Id = 1,
                UserId = RegisteredUser.Id,
                OrderDetailsId = Guid.Parse("085d317d-389b-48b2-8d82-dfb3d3de632a")
            };

            OrderTwo = new Order
            {
                Id = 2,
                UserId = RegisteredUser.Id,
                OrderDetailsId = Guid.Parse("0ac77787-c4d5-412c-b9df-d3c2a76b4e2d")
            };

            dbContext.Users.Add(SellerUser);
            dbContext.Users.Add(SellerUserTwo);
            dbContext.Users.Add(RegisteredUser);
            dbContext.Sellers.Add(Seller);
            dbContext.Sellers.Add(SellerTwo);
            dbContext.Products.Add(Product);
            dbContext.Products.Add(ProductTwo);
            dbContext.Products.Add(ProductThree);
            dbContext.Brands.Add(Brand);
            dbContext.Brands.Add(BrandTwo);
            dbContext.Categories.Add(Category);
            dbContext.Categories.Add(CategoryTwo);
            dbContext.Orders.Add(Order);
            dbContext.Orders.Add(OrderTwo);
            dbContext.OrdersDetails.Add(OrderDetails);
            dbContext.OrdersDetails.Add(OrderDetailsTwo);

            dbContext.SaveChanges();
        }
    }
}
