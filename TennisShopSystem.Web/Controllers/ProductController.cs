namespace TennisShopSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TennisShopSystem.Services.Data.Interfaces;
    using Infrastructure.Extensions;
    using ViewModels.Product;
    using TennisShopSystem.DataTransferObjects.Product;

    using static Common.GeneralApplicationConstants;
    using static Common.NotificationMessagesConstants;
    using TennisShopSystem.Web.ViewModels.Seller;
    using TennisShopSystem.Web.ViewModels.Category;
    using TennisShopSystem.Web.ViewModels.Brand;
    using TennisShopSystem.DataTransferObjects.Brand;
    using TennisShopSystem.DataTransferObjects.Category;

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
            AllProductQueryDto model = new()
            {
                Category = queryModel.Category,
                Brand = queryModel.Brand,
                SearchString = queryModel.SearchString,
                ProductSorting = (DataTransferObjects.Product.Enums.ProductSorting)queryModel.ProductSorting,
                CurrentPage = queryModel.CurrentPage,
                ProductsPerPage = queryModel.ProductsPerPage
            };

            AllProductsFilteredAndPagedDto serviceModel = await this.productService.AllAsync(model);

            foreach (ProductAllDto modelDto in serviceModel.Products)
            {
                ProductAllViewModel productModel = new()
                {
                    Id = modelDto.Id,
                    Title = modelDto.Title,
                    Description = modelDto.Description,
                    ImageUrl = modelDto.ImageUrl,
                    Price = modelDto.Price,
                    AvailableQuantity = modelDto.AvailableQuantity,
                    IsAvailable = modelDto.IsAvailable,
                    SoldItems = modelDto.SoldItems
                };

                queryModel.Products.Add(productModel);
            }

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
                ProductFormModel model = new();

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

                model.Brands = modelBrands;
                model.Categories = modelCategories;

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

                model.Brands = modelBrands;
                model.Categories = modelCategories;

                return this.View(model);
            }

            try
            {
                string? sellerId = await this.sellerService
                    .GetSellerIdByUserIdAsync(this.User.GetId()!);

                ProductFormDto productFormDto = new()
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    Price = model.Price,
                    AvailableQuantity = model.AvailableQuantity,
                    BrandId = model.BrandId,
                    CategoryId = model.CategoryId
                };

                string productId = await this.productService.CreateAndReturnIdAsync(productFormDto, sellerId!);

                this.TempData[SuccessMessage] = "Product was added successfully!";

                return RedirectToAction("Details", "Product", new { id= productId });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, 
                    "Unexpected error occurred while trying to add your new product! Please try again later or contact administrator!");

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

                model.Brands = modelBrands;
                model.Categories = modelCategories;

                return this.View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            string? userId = this.User.GetId();

            bool productExists;

            if (userId == null)
            {
                 productExists = await this.productService.ExistsByIdAsync(id);
            }
            else
            {
                productExists = await this.productService.ExistsBySellerIdAsync(id);
            }

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return RedirectToAction("All", "Product");
            }

            try
            {
                ProductDetailsDto detailsDto = await this.productService
                .GetDetailsByIdAsync(id);

                SellerInfoOnProductViewModel seller = new()
                {
                    Email = detailsDto.Seller.Email,
                    PhoneNumber = detailsDto.Seller.PhoneNumber
                };

                ProductDetailsViewModel viewModel = new()
                {
                    Id = detailsDto.Id,
                    Title = detailsDto.Title,
                    Description = detailsDto.Description,
                    ImageUrl = detailsDto.ImageUrl,
                    Price = detailsDto.Price,
                    AvailableQuantity = detailsDto.AvailableQuantity,
                    IsAvailable = detailsDto.IsAvailable,
                    Category = detailsDto.Category,
                    Brand = detailsDto.Brand,
                    Seller = seller
                };

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
                .ExistsBySellerIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return RedirectToAction("All", "Product");
            }

            bool isUserSeller = await this.sellerService
                .SellerExistByUserIdAsync(this.User.GetId()!);

            if (!isUserSeller && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to edit product info!";

                this.RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

            bool isSellerOwner = await this.productService
                .IsSellerWithIdOwnerOfProductWithIdAsync(id, sellerId!);

            if (!isSellerOwner && !this.User.IsAdmin()) 
            {
                this.TempData[ErrorMessage] = "You must be the seller of the product you want to edit!";

                return this.RedirectToAction("Mine", "Product");
            }

            try
            {
                ProductFormDto productDto = await this.productService
                .GetProductForEditByIdAsync(id);

                ProductFormModel formModel = new()
                {
                    Title = productDto.Title,
                    Description = productDto.Description,
                    ImageUrl = productDto.ImageUrl,
                    Price = productDto.Price,
                    AvailableQuantity = productDto.AvailableQuantity,
                    IsAvailable = productDto.IsAvailable,
                    BrandId = productDto.BrandId,
                    CategoryId = productDto.CategoryId
                };

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

                formModel.Categories = modelCategories;
                formModel.Brands = modelBrands;

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

                model.Categories = modelCategories;
                model.Brands = modelBrands;
                
                return this.View(model);
            }

            bool productExists = await this.productService
                .ExistsBySellerIdAsync(id);

            if (!productExists)
            {
                this.TempData[ErrorMessage] = "Product with the provided id does not exist!";

                return RedirectToAction("All", "Product");
            }

            bool isUserSeller = await this.sellerService
                .SellerExistByUserIdAsync(this.User.GetId()!);

            if (!isUserSeller && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to edit product info!";

                this.RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

            bool isSellerOwner = await this.productService
                .IsSellerWithIdOwnerOfProductWithIdAsync(id, sellerId!);

            if (!isSellerOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the seller of the product you want to edit!";

                return this.RedirectToAction("Mine", "Product");
            }

            ProductFormDto productDto = new()
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                AvailableQuantity = model.AvailableQuantity,
                IsAvailable = model.IsAvailable,
                BrandId = model.BrandId,
                CategoryId = model.CategoryId
            };

            try
            {
                await this.productService.EditProductByIdAndFormModelAsync(id, productDto);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "An unexpected error occurred while trying to update your product! Please try again later or contact administrator!");

                productDto.Categories = await this.categoryService.AllCategoriesAsync();
                productDto.Brands = await this.brandService.AllBrandsAsync();

                model.Categories = (IEnumerable<ProductSelectCategoryFormModel>)productDto.Categories;
                model.Brands = (IEnumerable<ProductSelectBrandFormModel>)productDto.Brands;

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Product was edited successfully!";

            return RedirectToAction("Details", "Product", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
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

            if (!isUserSeller && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to delete the product!";

                this.RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

            bool isSellerOwner = await this.productService
                .IsSellerWithIdOwnerOfProductWithIdAsync(id, sellerId!);

            if (!isSellerOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the seller of the product you want to delete it!";

                return this.RedirectToAction("Mine", "Product");
            }

            try
            {
                ProductPreDeleteDetailsDto productDto =
                    await this.productService.GetProductForDeleteByIdAsync(id);

                ProductPreDeleteDetailsViewModel viewModel = new()
                {
                    Title = productDto.Title,
                    ImageUrl = productDto.ImageUrl
                };

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, ProductPreDeleteDetailsViewModel viewModel)
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

            if (!isUserSeller && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = this.TempData[ErrorMessage] = "You must become a seller in order to delete the product!";

                this.RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

            bool isSellerOwner = await this.productService
                .IsSellerWithIdOwnerOfProductWithIdAsync(id, sellerId!);

            if (!isSellerOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the seller of the product you want to delete it!";

                return this.RedirectToAction("Mine", "Product");
            }

            try
            {
                await this.productService.DeleteProductByIdAsync(id);

                this.TempData[WarningMessage] = "The product was successfully deleted!";
                return this.RedirectToAction("Mine", "Product");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<ProductAllViewModel> myProducts = new List<ProductAllViewModel>();

            string userId = this.User.GetId()!;

            if (this.User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Mine", "Product", new { Area = AdminAreaName });
            }

            bool isSeller = await this.sellerService
                .SellerExistByUserIdAsync(userId);

            if (!isSeller)
            {
                this.TempData[ErrorMessage] = "You must become a seller in order to view your products!";

                return this.RedirectToAction("Become", "Seller");
            }

            try
            {
                string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(userId);

                List<ProductAllDto> myDtoProducts = new();

                myDtoProducts.AddRange(await this.productService.AllBySellerIdAsync(sellerId!));

                foreach (ProductAllDto productDto in myDtoProducts)
                {
                    ProductAllViewModel viewModel = new()
                    {
                        Id = productDto.Id,
                        Title = productDto.Title,
                        Description = productDto.Description,
                        ImageUrl = productDto.ImageUrl,
                        Price = productDto.Price,
                        AvailableQuantity = productDto.AvailableQuantity,
                        IsAvailable = productDto.IsAvailable,
                        SoldItems = productDto.SoldItems
                    };

                    myProducts.Add(viewModel);
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
