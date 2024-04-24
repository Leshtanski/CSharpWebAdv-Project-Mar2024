namespace TennisShopSystem.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using TennisShopSystem.Data;
    using Data;
    using Web.ViewModels.Home;
    using Web.ViewModels.Product;
    using Web.ViewModels.Seller;
    
    using static DatabaseSeeder;
    using TennisShopSystem.DataTransferObjects;
    using TennisShopSystem.DataTransferObjects.Product;

    public class ProductServiceTests
    {
        private DbContextOptions<TennisShopDbContext> dbOptions;
        private TennisShopDbContext dbContext;
        private ProductService productService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            this.dbOptions = new DbContextOptionsBuilder<TennisShopDbContext>()
                .UseInMemoryDatabase("TennisShopInMemory" + Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new TennisShopDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(dbContext);

            this.productService = new ProductService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AllBySellerIdAsyncShouldReturnAllSellerProducts()
        {
            ProductAllViewModel[] allSellerProducts =
            {
                new()
                {
                    Id = "e1ec4bb2-d890-40e7-bf4c-0ea73990c496",
                    Title = "Head Tennis Racket",
                    Description = "This is the Prestige Head Tennis Racket! You will have a 200 km serve with it!",
                    ImageUrl = "https://cdn.shopify.com/s/files/1/0602/4763/8188/products/prestige-tour_1_768x.jpg?v=1691793838",
                    Price = 140.00m,
                    AvailableQuantity = 0,
                    IsAvailable = false
                },
                new()
                {
                    Id = "8ce39907-3ca4-44fd-955b-c30ccdeb67b8",
                    Title = "Adidas Headband",
                    Description = "This is an authentic headband for tennis! You will not sweat your eyes with it!",
                    ImageUrl = "https://i5.walmartimages.com/seo/Adidas-Tennis-Tie-Back-Aeroready-Reversible-Headband-Red-Black_2976cee6-649c-4be5-bc9c-5104efe73a01.120a9790acefba7ae71b7e4ef02a679a.jpeg?odnHeight=640&odnWidth=640&odnBg=FFFFFF",
                    Price = 20.00m,
                    AvailableQuantity = 0,
                    IsAvailable = false
                }
            };

            IEnumerable<ProductAllDto> allSellerProductsDb =
                await this.productService.AllBySellerIdAsync(Seller.Id.ToString());

            var allSellerProductsDbConverted = allSellerProductsDb.ToArray();

            Assert.AreEqual(allSellerProducts[0].Id, allSellerProductsDbConverted[0].Id);
            Assert.AreEqual(allSellerProducts[0].Title, allSellerProductsDbConverted[0].Title);
            Assert.AreEqual(allSellerProducts[0].Description, allSellerProductsDbConverted[0].Description);
            Assert.AreEqual(allSellerProducts[0].ImageUrl, allSellerProductsDbConverted[0].ImageUrl);
            Assert.AreEqual(allSellerProducts[0].Price, allSellerProductsDbConverted[0].Price);
            Assert.AreEqual(allSellerProducts[0].AvailableQuantity, allSellerProductsDbConverted[0].AvailableQuantity);
            Assert.AreEqual(allSellerProducts[0].IsAvailable, allSellerProductsDbConverted[0].IsAvailable);
            Assert.AreEqual(allSellerProducts[1].Id, allSellerProductsDbConverted[1].Id);
            Assert.AreEqual(allSellerProducts[1].Title, allSellerProductsDbConverted[1].Title);
            Assert.AreEqual(allSellerProducts[1].Description, allSellerProductsDbConverted[1].Description);
            Assert.AreEqual(allSellerProducts[1].ImageUrl, allSellerProductsDbConverted[1].ImageUrl);
            Assert.AreEqual(allSellerProducts[1].Price, allSellerProductsDbConverted[1].Price);
            Assert.AreEqual(allSellerProducts[1].AvailableQuantity, allSellerProductsDbConverted[1].AvailableQuantity);
            Assert.AreEqual(allSellerProducts[1].IsAvailable, allSellerProductsDbConverted[1].IsAvailable);
        }

        [Test]
        public async Task AllByUserIdAsyncShouldReturnAllOrderedItems()
        {
            ProductAllViewModel[] allUserProducts =
            {
                new()
                {
                    Id = "e1ec4bb2-d890-40e7-bf4c-0ea73990c496",
                    Title = "Head Tennis Racket",
                    Description = "This is the Prestige Head Tennis Racket! You will have a 200 km serve with it!",
                    ImageUrl = "https://cdn.shopify.com/s/files/1/0602/4763/8188/products/prestige-tour_1_768x.jpg?v=1691793838",
                    Price = 140.00m,
                    AvailableQuantity = 0,
                    IsAvailable = false
                },
                new()
                {
                    Id = "0a5f0d11-c157-4ce0-a552-1f8bb032cee2",
                    Title = "Adidas Headband",
                    Description = "This is an authentic headband for tennis! You will not sweat your eyes with it!",
                    ImageUrl = "https://i5.walmartimages.com/seo/Adidas-Tennis-Tie-Back-Aeroready-Reversible-Headband-Red-Black_2976cee6-649c-4be5-bc9c-5104efe73a01.120a9790acefba7ae71b7e4ef02a679a.jpeg?odnHeight=640&odnWidth=640&odnBg=FFFFFF",
                    Price = 20.00m,
                    AvailableQuantity = 0,
                    IsAvailable = false
                },
                new()
                {
                    Id = "8ce39907-3ca4-44fd-955b-c30ccdeb67b8",
                    Title = "Adidas Headband",
                    Description = "This is an authentic headband for tennis! You will not sweat your eyes with it!",
                    ImageUrl = "https://i5.walmartimages.com/seo/Adidas-Tennis-Tie-Back-Aeroready-Reversible-Headband-Red-Black_2976cee6-649c-4be5-bc9c-5104efe73a01.120a9790acefba7ae71b7e4ef02a679a.jpeg?odnHeight=640&odnWidth=640&odnBg=FFFFFF",
                    Price = 20.00m,
                    AvailableQuantity = 0,
                    IsAvailable = false
                }
            };

            IEnumerable<ProductAllDto> allUserItems 
                = await this.productService.AllByUserIdAsync(RegisteredUser.Id.ToString());

            var allUserItemsConverted = allUserItems.ToArray();

            Assert.AreEqual(allUserProducts[0].Id, allUserItemsConverted[0].Id);
            Assert.AreEqual(allUserProducts[0].Title, allUserItemsConverted[0].Title);
            Assert.AreEqual(allUserProducts[0].Description, allUserItemsConverted[0].Description);
            Assert.AreEqual(allUserProducts[0].ImageUrl, allUserItemsConverted[0].ImageUrl);
            Assert.AreEqual(allUserProducts[0].Price, allUserItemsConverted[0].Price);
            Assert.AreEqual(allUserProducts[0].AvailableQuantity, allUserItemsConverted[0].AvailableQuantity);
            Assert.AreEqual(allUserProducts[0].IsAvailable, allUserItemsConverted[0].IsAvailable);
            Assert.AreEqual(allUserProducts[1].Id, allUserItemsConverted[1].Id);
            Assert.AreEqual(allUserProducts[1].Title, allUserItemsConverted[1].Title);
            Assert.AreEqual(allUserProducts[1].Description, allUserItemsConverted[1].Description);
            Assert.AreEqual(allUserProducts[1].ImageUrl, allUserItemsConverted[1].ImageUrl);
            Assert.AreEqual(allUserProducts[1].Price, allUserItemsConverted[1].Price);
            Assert.AreEqual(allUserProducts[1].AvailableQuantity, allUserItemsConverted[1].AvailableQuantity);
            Assert.AreEqual(allUserProducts[1].IsAvailable, allUserItemsConverted[1].IsAvailable);
            Assert.AreEqual(allUserProducts[2].Id, allUserItemsConverted[2].Id);
            Assert.AreEqual(allUserProducts[2].Title, allUserItemsConverted[2].Title);
            Assert.AreEqual(allUserProducts[2].Description, allUserItemsConverted[2].Description);
            Assert.AreEqual(allUserProducts[2].ImageUrl, allUserItemsConverted[2].ImageUrl);
            Assert.AreEqual(allUserProducts[2].Price, allUserItemsConverted[2].Price);
            Assert.AreEqual(allUserProducts[2].AvailableQuantity, allUserItemsConverted[2].AvailableQuantity);
            Assert.AreEqual(allUserProducts[2].IsAvailable, allUserItemsConverted[2].IsAvailable);
        }

        [Test]
        public async Task DeleteProductByIdAsyncShouldChangeIsAvailableToFalse()
        {
            Product.IsAvailable = true;
            await this.dbContext.SaveChangesAsync();

            await this.productService.DeleteProductByIdAsync(Product.Id.ToString());

            Assert.AreEqual(false, Product.IsAvailable);
        }

        [Test]
        public async Task EditProductByIdAndFormModelAsyncShouldChangeData()
        {
            ProductFormModel model = new()
            {
                Title = "Nike Shoes",
                Description = "These are high quality Nike Tennis Shoes! Only the pro's play with them!",
                ImageUrl = "https://static.nike.com/a/images/c_limit,w_592,f_auto/t_product_v1/729b888b-db9a-4caa-b67e-433b5f9811bb/nikecourt-zoom-vapor-cage-4-rafa-hard-court-tennis-shoes-1t7FzV.png",
                Price = 100,
                AvailableQuantity = 5,
                IsAvailable = true,
                BrandId = 1,
                CategoryId = 2,
            };

            await this.productService.EditProductByIdAndFormModelAsync(Product.Id.ToString(), model);

            Assert.AreEqual(Product.Title, model.Title);
            Assert.AreEqual(Product.Description, model.Description);
            Assert.AreEqual(Product.ImageUrl, model.ImageUrl);
            Assert.AreEqual(Product.Price, model.Price);
            Assert.AreEqual(Product.AvailableQuantity, model.AvailableQuantity);
            Assert.AreEqual(Product.IsAvailable, model.IsAvailable);
            Assert.AreEqual(Product.BrandId, model.BrandId);
            Assert.AreEqual(Product.CategoryId, model.CategoryId);
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnTrueWhenExists()
        {
            Product.IsAvailable = true;

            await this.dbContext.SaveChangesAsync();

            bool result = await this.productService.ExistsByIdAsync(Product.Id.ToString());

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnFalseWhenItDoesNotExists()
        {
            bool result = await this.productService.ExistsByIdAsync("58b86320-4cc4-4c28-b649-1c5b212cb074");

            Assert.IsFalse(result);
        }

        [Test]
        public async Task ExistsByIdAsyncShouldReturnFalseWhenExistsAndIsAvailableIsFalse()
        {
            bool result = await this.productService.ExistsByIdAsync(ProductTwo.Id.ToString());

            Assert.IsFalse(result);
        }

        [Test]
        public async Task ExistsBySellerIdAsyncShouldReturnTrueWhenExists()
        {
            bool result =
                await this.productService.ExistsBySellerIdAsync(Product.Id.ToString());

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsBySellerIdAsyncShouldReturnFalseWhenDoesNotExist()
        {
            bool result =
                await this.productService.ExistsBySellerIdAsync("fafa316c-4534-428a-9147-1173edf6ea3c");

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetDetailsByIdAsyncShouldReturnCorrectProductDetailsViewModel()
        {
            ProductDetailsViewModel model = new ProductDetailsViewModel
            {
                Id = Product.Id.ToString(),
                Title = Product.Title,
                Description = Product.Description,
                ImageUrl = Product.ImageUrl,
                Price = Product.Price,
                Category = Category.Name,
                Brand = Brand.Name,
                Seller = new SellerInfoOnProductViewModel
                {
                    Email = SellerUser.Email,
                    PhoneNumber = Seller.PhoneNumber
                }
            };

            Product.Brand = Brand;
            Product.Category = Category;
            Product.IsAvailable = true;

            await this.dbContext.SaveChangesAsync();

            ProductDetailsDto methodModel = await this.productService.GetDetailsByIdAsync(Product.Id.ToString());

            Assert.AreEqual(model.Id, methodModel.Id);
            Assert.AreEqual(model.Title, methodModel.Title);
            Assert.AreEqual(model.Description, methodModel.Description);
            Assert.AreEqual(model.ImageUrl, methodModel.ImageUrl);
            Assert.AreEqual(model.Price, methodModel.Price);
            Assert.AreEqual(model.Category, methodModel.Category);
            Assert.AreEqual(model.Brand, methodModel.Brand);
            Assert.AreEqual(model.Seller.Email, methodModel.Seller.Email);
            Assert.AreEqual(model.Seller.PhoneNumber, methodModel.Seller.PhoneNumber);
        }

        [Test]
        public async Task GetProductForDeleteByIdAsyncShouldReturnCorrectProductPreDeleteDetailsViewModel()
        {
            ProductPreDeleteDetailsViewModel model = new()
            {
                Title = Product.Title,
                ImageUrl = Product.ImageUrl
            };

            Product.IsAvailable = true;
            await this.dbContext.SaveChangesAsync();

            ProductPreDeleteDetailsDto methodModel =
                await this.productService.GetProductForDeleteByIdAsync(Product.Id.ToString());

            Assert.AreEqual(model.Title, methodModel.Title);
            Assert.AreEqual(model.ImageUrl, methodModel.ImageUrl);
        }

        [Test]
        public async Task GetProductForEditByIdAsyncShouldReturnCorrectProductFormModel()
        {
            Product.Brand = Brand;
            Product.BrandId = Brand.Id;
            Product.Category = Category;
            Product.CategoryId = Category.Id;

            await this.dbContext.SaveChangesAsync();

            ProductFormModel model = new()
            {
                Title = Product.Title,
                Description = Product.Description,
                ImageUrl = Product.ImageUrl,
                Price = Product.Price,
                AvailableQuantity = Product.AvailableQuantity,
                CategoryId = Product.CategoryId,
                BrandId = Product.BrandId
            };

            ProductFormDto methodModel =
                await this.productService.GetProductForEditByIdAsync(Product.Id.ToString());

            Assert.AreEqual(model.Title, methodModel.Title);
            Assert.AreEqual(model.Description, methodModel.Description);
            Assert.AreEqual(model.ImageUrl, methodModel.ImageUrl);
            Assert.AreEqual(model.Price, methodModel.Price);
            Assert.AreEqual(model.AvailableQuantity, methodModel.AvailableQuantity);
            Assert.AreEqual(model.CategoryId, methodModel.CategoryId);
            Assert.AreEqual(model.BrandId, methodModel.BrandId);
        }

        [Test]
        public async Task IsSellerWithIdOwnerOfProductWithIdAsyncShouldReturnTrue()
        {
            bool result =
                await this.productService.IsSellerWithIdOwnerOfProductWithIdAsync(Product.Id.ToString(),
                    Seller.Id.ToString());

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsSellerWithIdOwnerOfProductWithIdAsyncShouldReturnFalse()
        {
            bool result =
                await this.productService.IsSellerWithIdOwnerOfProductWithIdAsync(Product.Id.ToString(),
                    "4d05148b-1a72-48d6-8105-567efed42765");

            Assert.IsFalse(result);
        }

        [Test]
        public async Task LastThreeProductsAsyncShouldReturnLastThreeProducts()
        {
            Product.IsAvailable = true;
            ProductTwo.IsAvailable = true;
            ProductThree.IsAvailable = true;

            await this.dbContext.SaveChangesAsync();

            IndexViewModel[] models =
            {
                new()
                {
                    Id = Product.Id.ToString(),
                    Title = Product.Title,
                    ImageUrl = Product.ImageUrl
                },
                new()
                {
                    Id = ProductTwo.Id.ToString(),
                    Title = ProductTwo.Title,
                    ImageUrl = ProductTwo.ImageUrl
                },
                new()
                {
                    Id = ProductThree.Id.ToString(),
                    Title = ProductThree.Title,
                    ImageUrl = ProductThree.ImageUrl
                }
            };

            IEnumerable<IndexModelDto> methodModels =
                await this.productService.LastThreeProductsAsync();

            var methodModelsConverted = methodModels.ToArray();

            Assert.AreEqual(models[0].Id, methodModelsConverted[0].Id);
            Assert.AreEqual(models[0].Title, methodModelsConverted[0].Title);
            Assert.AreEqual(models[0].ImageUrl, methodModelsConverted[0].ImageUrl);
            Assert.AreEqual(models[1].Id, methodModelsConverted[1].Id);
            Assert.AreEqual(models[1].Title, methodModelsConverted[1].Title);
            Assert.AreEqual(models[1].ImageUrl, methodModelsConverted[1].ImageUrl);
            Assert.AreEqual(models[2].Id, methodModelsConverted[2].Id);
            Assert.AreEqual(models[2].Title, methodModelsConverted[2].Title);
            Assert.AreEqual(models[2].ImageUrl, methodModelsConverted[2].ImageUrl);
        }
    }
}
