namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Data.Models;
    using Infrastructure.Extensions;
    using ViewModels.OrderDetails;
    using TennisShopSystem.Services.Data.Interfaces;

    using static Common.NotificationMessagesConstants;
    using TennisShopSystem.Web.ViewModels.Category;
    using TennisShopSystem.Web.ViewModels.Brand;
    using TennisShopSystem.DataTransferObjects.Order;
    using TennisShopSystem.DataTransferObjects.Brand;
    using TennisShopSystem.DataTransferObjects.Category;

    [Authorize]
    public class OrderDetailsController : Controller
    {
        private readonly ISellerService sellerService;
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly IOrderService orderService;

        public OrderDetailsController(
                ISellerService sellerService,
                IBrandService brandService,
                ICategoryService categoryService,
                IOrderService orderService)
        {
            this.sellerService = sellerService;
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.orderService = orderService;
        }

        public async Task<IActionResult> CurrentOrderDetails(OrderDetailsFormModel formModel)
        {
            var currentCartItems = HttpContext.Session
                .Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

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

            OrderDetailsViewModel model = new()
            {
                Id = formModel.OrderId,
                FirstName = formModel.FirstName,
                LastName = formModel.LastName,
                Address = formModel.Address,
                PhoneNumber = formModel.PhoneNumber,
                EmailAddress = formModel.EmailAddress,
                Comment = formModel.Comment,
                TotalPrice = formModel.TotalPrice,
                Items = currentCartItems,
                Categories = modelCategories,
                Brands = modelBrands
            };

            HttpContext.Session.Set("Cart", new List<ShoppingCartItem>());

            return View(model);
        }

        public async Task<IActionResult> MyOrdersDetails()
        {
            string userId = this.User.GetId()!;

            bool isSeller = await sellerService.SellerExistByUserIdAsync(userId);

            if (isSeller && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must not be a seller to view orders!";

                return this.RedirectToAction("Index", "Home");
            }

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

            AllOrdersDto ordersDto = await this.orderService.GetAllOrdersByUserIdAsync(userId);

            AllOrdersViewModel viewModel = new();

            foreach (var order in ordersDto.Orders)
            {
                OrderDetailsViewModel model = new()
                {
                    Id = order.Id,
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    Address = order.Address,
                    PhoneNumber = order.PhoneNumber,
                    EmailAddress = order.EmailAddress,
                    TotalPrice = order.TotalPrice,
                    Items = order.Items,
                    OrderRegisteredOn = order.OrderRegisteredOn,
                    Categories = modelCategories,
                    Brands = modelBrands
                };

                viewModel.Orders.Add(model);
            }

            return View(viewModel);
        }
    }
}
