namespace TennisShopSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using TennisShopSystem.DataTransferObjects.Brand;
    using TennisShopSystem.DataTransferObjects.Category;
    using TennisShopSystem.DataTransferObjects.Order;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Web.ViewModels.Brand;
    using TennisShopSystem.Web.ViewModels.Category;
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
            AllOrdersDto AllOrdersDto = await this.orderService.GetAllOrdersAsync();

            AllOrdersViewModel viewModel = new();

            IEnumerable<ProductSelectBrandFormDto> brands = await this.brandService.AllBrandsAsync();
            IEnumerable<ProductSelectCategoryFormDto> categories = await this.categoryService.AllCategoriesAsync();

            IEnumerable<ProductSelectBrandFormModel> modelBrands = brands
                .Select(b => new ProductSelectBrandFormModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToArray();

            IEnumerable<ProductSelectCategoryFormModel> modelCategories = categories
                .Select(c => new ProductSelectCategoryFormModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArray();

            foreach (OrderDetailsDto orderDto in AllOrdersDto.Orders)
            {
                OrderDetailsViewModel model = new()
                {
                    Id = orderDto.Id,
                    FirstName = orderDto.FirstName,
                    LastName = orderDto.LastName,
                    Address = orderDto.Address,
                    PhoneNumber = orderDto.PhoneNumber,
                    EmailAddress = orderDto.EmailAddress,
                    Comment = orderDto.Comment,
                    TotalPrice = orderDto.TotalPrice,
                    Items = orderDto.Items,
                    OrderRegisteredOn = orderDto.OrderRegisteredOn,
                    Brands = modelBrands,
                    Categories = modelCategories
                };

                viewModel.Orders.Add(model);
            }

            return View(viewModel);
        }
    }
}
