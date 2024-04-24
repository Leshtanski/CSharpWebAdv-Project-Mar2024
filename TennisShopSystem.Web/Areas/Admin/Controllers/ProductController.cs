namespace TennisShopSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Product;
    using TennisShopSystem.Services.Data.Interfaces;
    using Infrastructure.Extensions;
    using TennisShopSystem.DataTransferObjects.Product;

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

            MyProductsDto productDto = new()
            {
                AddedProducts = await this.productService.AllBySellerIdAsync(sellerId!),
                BoughtProducts = await this.productService.AllByUserIdAsync(this.User.GetId()!)
            };

            IEnumerable<ProductAllViewModel> addedProducts = productDto.AddedProducts
                .Select(p => new ProductAllViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    AvailableQuantity = p.AvailableQuantity,
                    IsAvailable = p.IsAvailable,
                    SoldItems = p.SoldItems
                })
                .ToArray();
            
            IEnumerable<ProductAllViewModel> boughtProducts = productDto.BoughtProducts
                .Select(p => new ProductAllViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    AvailableQuantity = p.AvailableQuantity,
                    IsAvailable = p.IsAvailable,
                    SoldItems = p.SoldItems
                })
                .ToArray();

            MyProductsViewModel model = new()
            {
                AddedProducts = addedProducts,
                BoughtProducts = boughtProducts
            };

            return View(model);
        }
    }
}
