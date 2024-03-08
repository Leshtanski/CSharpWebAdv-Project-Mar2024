namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Web.Infrastructure.Extensions;
    using TennisShopSystem.Web.ViewModels.Product;

    using static TennisShopSystem.Common.NotificationMessagesConstants;

    [Authorize]
    public class ProductController : Controller
    {
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly ISellerService sellerService;    

        public ProductController(IBrandService brandService, ICategoryService categoryService, ISellerService sellerService)
        {
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.sellerService = sellerService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isSeller = await sellerService.SellerExistByUserIdAsync(this.User.GetId()!);
            
            if (!isSeller)
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to add new products!";

                return this.RedirectToAction("Become", "Seller");
            }

            ProductFormModel model = new()
            {
                Brands = await this.brandService.AllBrandsAsync(),
                Categories = await this.categoryService.AllCategoriesAsync()
            };

            return View(model);
        }
    }
}
