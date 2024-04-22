namespace TennisShopSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Interfaces;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Web.ViewModels.OrderDetails;

    public class OrderController : BaseAdminController
    {
        private readonly IOrderService orderService;
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;

        public OrderController(IOrderService orderService, IBrandService brandService, ICategoryService categoryService)
        {
            this.orderService = orderService;
            this.brandService = brandService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> AllOrders()
        {
            AllOrdersViewModel model = await this.orderService.GetAllOrdersAsync();

            foreach(OrderDetailsViewModel viewModel in model.Orders)
            {
                viewModel.Brands = await this.brandService.AllBrandsAsync();
                viewModel.Categories = await this.categoryService.AllCategoriesAsync();
            }

            return View(model);
        }
    }
}
