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

        [Test]
        public async Task SellerExistsByPhoneNumberShouldReturnTrueWhenExists()
        {
            string existingSellerPhoneNumber = Seller.PhoneNumber;

            bool result = await this.sellerService.SellerExistsByPhoneNumberAsync(existingSellerPhoneNumber);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task SellerExistsByPhoneNumberShouldReturnFalseWhenNotExists()
        {
            string existingSellerPhoneNumber = "+359874601487";

            bool result = await this.sellerService.SellerExistsByPhoneNumberAsync(existingSellerPhoneNumber);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsSellerOwnerOfProductShouldReturnTrueWhenSellerIsOwner()
        {
            string productId = Product.Id.ToString();

            bool result =
                await this.sellerService.IsSellerWithUserIdOwnerOfProductWithIdAsync(productId, Seller.UserId.ToString());

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsSellerOwnerOfProductShouldReturnFalseWhenSellerIsNotOwner()
        {
            string productId = Product.Id.ToString();

            bool result =
                await this.sellerService.IsSellerWithUserIdOwnerOfProductWithIdAsync(productId, "cff480c0-7555-4aa6-b895-fae1beab652f");

            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsSellerOwnerOfProductShouldReturnFalseWhenUnExistingSeller()
        {
            string productId = Product.Id.ToString();

            bool result =
                await this.sellerService.IsSellerWithUserIdOwnerOfProductWithIdAsync(productId, "b4f453f6-0564-4505-b05c-82406d619ac9");

            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsSellerOwnerOfProductShouldReturnFalseWhenMissMatchOfCorrectIds()
        {
            string productId = Product.Id.ToString();

            bool result =
                await this.sellerService.IsSellerWithUserIdOwnerOfProductWithIdAsync(productId, SellerTwo.UserId.ToString());

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetSellerIdByUserIdShouldReturnIdWhenExistingSeller()
        {
            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(Seller.UserId.ToString());

            Assert.AreEqual(Seller.Id.ToString(), sellerId!);
        }

        [Test]
        public async Task GetSellerIdByUserIdShouldReturnIdWhenUnExistingSeller()
        {
            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync("9a4f188b-a129-4e21-9c41-90c7ba3860f7");

            Assert.AreEqual(null, sellerId);
        }
    }
}