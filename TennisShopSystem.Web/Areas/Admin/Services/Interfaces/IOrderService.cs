namespace TennisShopSystem.Web.Areas.Admin.Services.Interfaces
{
    using TennisShopSystem.Web.ViewModels.OrderDetails;

    public interface IOrderService
    {
        Task<AllOrdersViewModel> GetAllOrdersAsync();
    }
}
