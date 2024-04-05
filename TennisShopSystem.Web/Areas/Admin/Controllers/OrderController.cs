namespace TennisShopSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Interfaces;
    using TennisShopSystem.Web.ViewModels.OrderDetails;

    public class OrderController : BaseAdminController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<IActionResult> AllOrders()
        {
            AllOrdersViewModel model = await this.orderService.GetAllOrdersAsync();

            return View(model);
        }
    }
}
