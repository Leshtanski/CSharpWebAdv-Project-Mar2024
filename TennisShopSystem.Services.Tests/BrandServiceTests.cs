namespace TennisShopSystem.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using TennisShopSystem.Data;
    using Data;
    using Data.Interfaces;
    using Web.ViewModels.Brand;

    using static DatabaseSeeder;
    using TennisShopSystem.DataTransferObjects.Brand;

    public class BrandServiceTests
    {
        private DbContextOptions<TennisShopDbContext> dbOptions;
        private TennisShopDbContext dbContext;
        private IBrandService brandService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<TennisShopDbContext>()
                .UseInMemoryDatabase("TennisShopInMemory" + Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new TennisShopDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(dbContext);

            this.brandService = new BrandService(dbContext);
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task ExistsByBrandIdShouldReturnTrueWhenExists()
        {
            bool result = await this.brandService.ExistsByIdAsync(Brand.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsByBrandIdShouldReturnFalseWhenItDoesNotExist()
        {
            bool result = await this.brandService.ExistsByIdAsync(17);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task AllBrandNamesShouldReturnAllBrandNames()
        {
            ProductSelectBrandFormModel[] allBrands =
            {
                new()
                {
                    Id = 15,
                    Name = "Nike"
                },
                new()
                {
                    Id = 16,
                    Name = "Adidas"
                }
            };

            IEnumerable<string> allDbBrandNames = await this.brandService.AllBrandNamesAsync();

            var allDbBrandsArray = allDbBrandNames.ToArray();

            Assert.AreEqual(allBrands[0].Name, allDbBrandsArray[0]);
            Assert.AreEqual(allBrands[1].Name, allDbBrandsArray[1]);
        }

        [Test]
        public async Task AllBrandsAsyncShouldReturnAllBrands()
        {
            ProductSelectBrandFormModel[] allBrands =
            {
                new()
                {
                    Id = 15,
                    Name = "Nike"
                },
                new()
                {
                    Id = 16,
                    Name = "Adidas"
                }
            };

            IEnumerable<ProductSelectBrandFormDto> brands = await this.brandService.AllBrandsAsync();

            var allDbBrands = brands.ToArray();

            Assert.AreEqual(allBrands[0].Id, allDbBrands[0].Id);
            Assert.AreEqual(allBrands[0].Name, allDbBrands[0].Name);
            Assert.AreEqual(allBrands[1].Id, allDbBrands[1].Id);
            Assert.AreEqual(allBrands[1].Name, allDbBrands[1].Name);
        }
    }
}
