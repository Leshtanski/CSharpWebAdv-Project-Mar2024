namespace TennisShopSystem.Services.Data.Interfaces
{
    using TennisShopSystem.DataTransferObjects.Order;
    using TennisShopSystem.Web.ViewModels.OrderDetails;

    public interface IOrderService
    {
        Task<AllOrdersDto> GetAllOrdersAsync();

        Task<string[]> CreateAndSaveOrderAsync(OrderDetailsDto orderDto, string userId);

        Task<AllOrdersDto> GetAllOrdersByUserIdAsync(string userId);
    }
}
