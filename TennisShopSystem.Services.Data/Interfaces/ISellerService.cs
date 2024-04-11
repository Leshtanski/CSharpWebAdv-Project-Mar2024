namespace TennisShopSystem.Services.Data.Interfaces
{
    using Web.ViewModels.Seller;

    public interface ISellerService
    {
        Task<bool> SellerExistByUserIdAsync(string userId);
        Task<bool> SellerExistsByPhoneNumberAsync(string phoneNumber);
        Task Create(string userId, BecomeSellerFormModel model);

        Task<string?> GetSellerIdByUserIdAsync(string userId);

        Task<bool> IsSellerWithUserIdOwnerOfProductWithIdAsync(string productId, string userId);
    }
}
