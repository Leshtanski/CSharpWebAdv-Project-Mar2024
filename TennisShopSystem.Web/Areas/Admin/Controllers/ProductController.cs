namespace TennisShopSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Product;
    using TennisShopSystem.Services.Data.Interfaces;
    using Infrastructure.Extensions;

    public class ProductController : BaseAdminController
    {
        private readonly ISellerService sellerService;
        private readonly IProductService productService;

        public ProductController(ISellerService sellerService, IProductService productService)
        {
            this.sellerService = sellerService;
            this.productService = productService;
        }

        public async Task<IActionResult> Mine()
        {
            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

            MyProductsViewModel model = new()
            {
                AddedProducts = await this.productService.AllBySellerIdAsync(sellerId!),
                BoughtProducts = await this.productService.AllByUserIdAsync(this.User.GetId()!)
            };

            return View(model);
        }
    }
}
