namespace TennisShopSystem.Services.Data.Interfaces
{
    using TennisShopSystem.DataTransferObjects.Seller;

    public interface ISellerService
    {
        Task<bool> SellerExistByUserIdAsync(string userId);
        Task<bool> SellerExistsByPhoneNumberAsync(string phoneNumber);
        Task Create(string userId, BecomeSellerDto model);

        Task<string?> GetSellerIdByUserIdAsync(string userId);

        Task<bool> IsSellerWithUserIdOwnerOfProductWithIdAsync(string productId, string userId);
    }
}
