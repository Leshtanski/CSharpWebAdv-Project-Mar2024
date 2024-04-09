namespace TennisShopSystem.Services.Tests
{
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DatabaseSeeder
    {
        public static ApplicationUser SellerUser;
        public static ApplicationUser SellerUserTwo;
        public static ApplicationUser RegisteredUser;
        public static Seller Seller;
        public static Seller SellerTwo;
        public static Product Product;
        public static Product ProductTwo;

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
                Id = Guid.Parse("E1EC4BB2-D890-40E7-BF4C-0EA73990C496"),
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
                Id = Guid.Parse("0A5F0D11-C157-4CE0-A552-1F8BB032CEE2"),
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

            dbContext.Users.Add(SellerUser);
            dbContext.Users.Add(SellerUserTwo);
            dbContext.Users.Add(RegisteredUser);
            dbContext.Sellers.Add(Seller);
            dbContext.Sellers.Add(SellerTwo);
            dbContext.Products.Add(Product);
            dbContext.Products.Add(ProductTwo);

            dbContext.SaveChanges();
        }
    }
}
