namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Services.Data.Models.Product;
    using TennisShopSystem.Web.Infrastructure.Extensions;
    using TennisShopSystem.Web.ViewModels.Product;

    using static TennisShopSystem.Common.NotificationMessagesConstants;

    [Authorize]
    public class ProductController : Controller
    {
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly ISellerService sellerService;
        private readonly IProductService productService;

        public ProductController(IBrandService brandService, ICategoryService categoryService, ISellerService sellerService, IProductService productService)
        {
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.sellerService = sellerService;
            this.productService = productService;

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]AllProductQueryModel queryModel)
        {
            AllProductsFilteredAndPagedServiceModel serviceModel = await this.productService.AllAsync(queryModel);
            
            queryModel.Products = serviceModel.Products;
            queryModel.TotalProducts = serviceModel.TotalProductsCount;
            queryModel.Categories = await this.categoryService.AllCategoryNamesAsync();
            queryModel.Brands = await this.brandService.AllBrandNamesAsync();

            return this.View(queryModel);
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

        [HttpPost]
        public async Task<IActionResult> Add(ProductFormModel model)
        {
            bool isSeller = await sellerService.SellerExistByUserIdAsync(this.User.GetId()!);

            if (!isSeller)
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to add new products!";

                return this.RedirectToAction("Become", "Seller");
            }

            bool categoryExists =
                await this.categoryService.ExistsByIdAsync(model.CategoryId);

            if (!categoryExists)
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "The selected category does not exist!");
            }

            bool brandExists =
                await this.brandService.ExistsByIdAsync(model.BrandId);

            if (!brandExists)
            {
                this.ModelState.AddModelError(nameof(model.BrandId), "The selected brand does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();
                model.Brands = await this.brandService.AllBrandsAsync();

                return this.View(model);
            }

            try
            {
                string? sellerId = await this.sellerService
                    .GetSellerIdByUserIdAsync(this.User.GetId()!);

                await this.productService.CreateAsync(model, sellerId!);
            }
            catch (Exception _)
            {
                ModelState.AddModelError(string.Empty, 
                    "Unexpected error occurred while trying to add your new product! Please try again later or contact administrator!");

                model.Categories = await this.categoryService.AllCategoriesAsync();
                model.Brands = await this.brandService.AllBrandsAsync();

                return this.View(model);
            }

            return RedirectToAction("All", "Product");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            ProductDetailsViewModel? viewModel = await this.productService
                .GetDetailsByIdAsync(id);

            if (viewModel == null)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return RedirectToAction("All", "Product");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<ProductAllViewModel> myProducts = new List<ProductAllViewModel>();

            string userId = this.User.GetId()!;

            bool isSeller = await this.sellerService
                .SellerExistByUserIdAsync(userId);

            if (isSeller)
            {
                string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(userId);

                myProducts.AddRange(await this.productService.AllBySellerIdAsync(sellerId!));
            }
            else
            {
                myProducts.AddRange(await this.productService.AllByUserIdAsync(userId));
            }

            return this.View(myProducts);
        }
    }
}
