namespace TennisShopSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TennisShopSystem.Data;
    using TennisShopSystem.Data.Models;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Web.ViewModels.Seller;
    //using static TennisShopSystem.Common.EntityValidationConstants;

    public class SellerService : ISellerService
    {
        private readonly TennisShopDbContext dbContext;

        public SellerService(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(string userId, BecomeSellerFormModel model)
        {
            Seller newSeller = new Seller()
            {
                PhoneNumber = model.PhoneNumber,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Sellers.AddAsync(newSeller);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetSellerIdByUserIdAsync(string userId)
        {
            Seller? seller = await this.dbContext
                .Sellers
                .FirstOrDefaultAsync(s => s.UserId.ToString() == userId);

            if (seller == null)
            {
                return null;
            }

            return seller.Id.ToString();
        }

        public async Task<bool> IsSellerWithUserIdOwnerOfProductWithIdAsync(string productId, string userId)
        {
            var currentSeller = await this.dbContext
                .Sellers
                .FirstOrDefaultAsync(cs => cs.UserId.ToString() == userId);

            if (currentSeller == null)
            {
                return false;
            }

            var product = await this.dbContext
                .Products
                .FirstAsync(p => p.Id.ToString() == productId);

            return product.SellerId == currentSeller.Id;
        }

        public async Task<bool> HasPurchasesByUserIdAsync(string userId)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return false;
            }

            return user.BoughtProducts.Any();
        }

        public async Task<bool> SellerExistByUserIdAsync(string userId)
        {
            bool result = await dbContext
                .Sellers
                .AnyAsync(s => s.UserId.ToString() == userId);

            return result;
        }

        public async Task<bool> SellerExistsByPhoneNumberAsync(string phoneNumber)
        {
            bool result = await dbContext
                .Sellers
                .AnyAsync(s => s.PhoneNumber == phoneNumber);

            return result;
        }
    }
}
