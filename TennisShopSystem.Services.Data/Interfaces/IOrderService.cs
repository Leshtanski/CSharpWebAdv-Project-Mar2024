namespace TennisShopSystem.Services.Data.Interfaces
{
    using TennisShopSystem.Web.ViewModels.OrderDetails;

    public interface IOrderService
    {
        Task<AllOrdersViewModel> GetAllOrdersAsync();
    }
}
