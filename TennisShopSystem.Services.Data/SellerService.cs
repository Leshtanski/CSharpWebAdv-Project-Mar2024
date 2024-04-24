namespace TennisShopSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;
    using TennisShopSystem.Data;
    using TennisShopSystem.Data.Models;
    using Interfaces;
    using TennisShopSystem.DataTransferObjects.Seller;

    public class SellerService : ISellerService
    {
        private readonly TennisShopDbContext dbContext;

        public SellerService(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(string userId, BecomeSellerDto model)
        {
            Seller newSeller = new Seller
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
