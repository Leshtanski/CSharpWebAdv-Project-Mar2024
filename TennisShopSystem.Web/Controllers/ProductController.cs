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

            try
            {
                ProductFormModel model = new()
                {
                    Brands = await this.brandService.AllBrandsAsync(),
                    Categories = await this.categoryService.AllCategoriesAsync()
                };

                return View(model);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
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

                string productId = await this.productService.CreateAndReturnIdAsync(model, sellerId!);

                return RedirectToAction("Details", "Product", new { id= productId });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, 
                    "Unexpected error occurred while trying to add your new product! Please try again later or contact administrator!");

                model.Categories = await this.categoryService.AllCategoriesAsync();
                model.Brands = await this.brandService.AllBrandsAsync();

                return this.View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool productExists = await this.productService
                .ExistsByIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return RedirectToAction("All", "Product");
            }

            try
            {
                ProductDetailsViewModel viewModel = await this.productService
                .GetDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool productExists = await this.productService
                .ExistsByIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return RedirectToAction("All", "Product");
            }

            bool isUserSeller = await this.sellerService
                .SellerExistByUserIdAsync(this.User.GetId()!);

            if (!isUserSeller)
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to edit product info!";

                this.RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

            bool isSellerOwner = await this.productService
                .IsSellerWithIdOwnerOfProductWithIdAsync(id, sellerId!);

            if (!isSellerOwner) 
            {
                this.TempData[ErrorMessage] = "You must be the seller of the product you want to edit!";

                return this.RedirectToAction("Mine", "Product");
            }

            try
            {
                ProductFormModel formModel = await this.productService
                .GetProductForEditByIdAsync(id);

                formModel.Categories = await this.categoryService.AllCategoriesAsync();
                formModel.Brands = await this.brandService.AllBrandsAsync();

                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ProductFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();
                model.Brands = await this.brandService.AllBrandsAsync();

                return this.View(model);
            }

            bool productExists = await this.productService
                .ExistsByIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return RedirectToAction("All", "Product");
            }

            bool isUserSeller = await this.sellerService
                .SellerExistByUserIdAsync(this.User.GetId()!);

            if (!isUserSeller)
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to edit product info!";

                this.RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

            bool isSellerOwner = await this.productService
                .IsSellerWithIdOwnerOfProductWithIdAsync(id, sellerId!);

            if (!isSellerOwner)
            {
                this.TempData[ErrorMessage] = "You must be the seller of the product you want to edit!";

                return this.RedirectToAction("Mine", "Product");
            }

            try
            {
                await this.productService.EditProductByIdAndFormModel(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "An unexpected error occurred while trying to update your product! Please try again later or contact administrator!");
                
                model.Categories = await this.categoryService.AllCategoriesAsync();
                model.Brands = await this.brandService.AllBrandsAsync();

                return this.View(model);
            }

            return RedirectToAction("Details", "Product", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<ProductAllViewModel> myProducts = new List<ProductAllViewModel>();

            string userId = this.User.GetId()!;

            bool isSeller = await this.sellerService
                .SellerExistByUserIdAsync(userId);

            try
            {
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
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "An unexpected error occurred! Please try again later or contact administrator!";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
