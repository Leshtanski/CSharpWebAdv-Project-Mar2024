namespace TennisShopSystem.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using TennisShopSystem.Data;
    using Data;
    using Data.Interfaces;
    using static DatabaseSeeder;

    public class SellerServiceTests
    {
        private DbContextOptions<TennisShopDbContext> dbOptions;
        private TennisShopDbContext dbContext;
        private ISellerService sellerService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            this.dbOptions = new DbContextOptionsBuilder<TennisShopDbContext>()
                .UseInMemoryDatabase("TennisShopInMemory" + Guid.NewGuid().ToString())
                .Options;
            
            this.dbContext = new TennisShopDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(dbContext);

            this.sellerService = new SellerService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SellerExistsByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string existingSellerUserId = SellerUser.Id.ToString();

            bool result = await this.sellerService.SellerExistByUserIdAsync(existingSellerUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task SellerExistsByUserIdAsyncShouldReturnFalseWhenExists()
        {
            string existingSellerUserId = RegisteredUser.Id.ToString();

            bool result = await this.sellerService.SellerExistByUserIdAsync(existingSellerUserId);

            Assert.IsFalse(result);
        }
    }
}