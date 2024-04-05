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
        public static ApplicationUser RegisteredUser;
        public static Seller Seller;

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

            dbContext.Users.Add(SellerUser);
            dbContext.Users.Add(RegisteredUser);
            dbContext.Sellers.Add(Seller);

            dbContext.SaveChanges();
        }
    }
}
