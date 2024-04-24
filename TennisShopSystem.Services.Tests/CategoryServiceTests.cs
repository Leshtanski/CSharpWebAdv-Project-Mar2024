namespace TennisShopSystem.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TennisShopSystem.Data;
    using Data.Interfaces;
    using Data;
    using static DatabaseSeeder;
    using Web.ViewModels.Category;
    using TennisShopSystem.DataTransferObjects.Category;

    public class CategoryServiceTests
    {
        private DbContextOptions<TennisShopDbContext> dbOptions;
        private TennisShopDbContext dbContext;
        private ICategoryService categoryService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<TennisShopDbContext>()
                .UseInMemoryDatabase("TennisShopInMemory" + Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new TennisShopDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(dbContext);

            this.categoryService = new CategoryService(dbContext);
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task ExistsByCategoryIdShouldReturnTrueWhenExists()
        {
            bool result = await this.categoryService.ExistsByIdAsync(Category.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsByCategoryIdShouldReturnFalseWhenItDoesNotExist()
        {
            bool result = await this.categoryService.ExistsByIdAsync(17);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task AllCategoryNamesShouldReturnAllCategoryNames()
        {
            ProductSelectCategoryFormModel[] allCategories =
            {
                new()
                {
                    Id = 20,
                    Name = "Rackets"
                },
                new()
                {
                    Id = 21,
                    Name = "Balls"
                }
            };

            IEnumerable<string> allDbCategoryNames = await this.categoryService.AllCategoryNamesAsync();

            var allDbCategoriesArray = allDbCategoryNames.ToArray();

            Assert.AreEqual(allCategories[0].Name, allDbCategoriesArray[0]);
            Assert.AreEqual(allCategories[1].Name, allDbCategoriesArray[1]);
        }

        [Test]
        public async Task AllBrandsAsyncShouldReturnAllBrands()
        {
            ProductSelectCategoryFormModel[] allCategories =
            {
                new()
                {
                    Id = 20,
                    Name = "Rackets"
                },
                new()
                {
                    Id = 21,
                    Name = "Balls"
                }
            };

            IEnumerable<ProductSelectCategoryFormDto> categories = await this.categoryService.AllCategoriesAsync();

            var allDbCategories = categories.ToArray();

            Assert.AreEqual(allCategories[0].Id, allDbCategories[0].Id);
            Assert.AreEqual(allCategories[0].Name, allDbCategories[0].Name);
            Assert.AreEqual(allCategories[1].Id, allDbCategories[1].Id);
            Assert.AreEqual(allCategories[1].Name, allDbCategories[1].Name);
        }
    }
}
