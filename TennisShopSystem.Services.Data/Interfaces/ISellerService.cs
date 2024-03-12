namespace TennisShopSystem.Services.Data.Interfaces
{
    using TennisShopSystem.Web.ViewModels.Seller;

    public interface ISellerService
    {
        Task<bool> SellerExistByUserIdAsync(string userId);
        Task<bool> SellerExistsByPhoneNumberAsync(string phoneNumber);
        Task<bool> HasPurchasesByUserIdAsync(string userId);
        Task Create(string userId, BecomeSellerFormModel model);

        Task<string?> GetSellerIdByUserIdAsync(string userId);
    }
}
