namespace TennisShopSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using TennisShopSystem.Data;
    using TennisShopSystem.Services.Data.Interfaces;
    using TennisShopSystem.Web.ViewModels.Brand;

    public class BrandService : IBrandService
    {
        private readonly TennisShopDbContext dbContext;

        public BrandService(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductSelectBrandFormModel>> AllBrandsAsync()
        {
            IEnumerable<ProductSelectBrandFormModel> allBrands = await this.dbContext
                .Brands
                .AsNoTracking()
                .Select(c => new ProductSelectBrandFormModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();

            return allBrands;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .Brands
                .AnyAsync(c => c.Id == id);

            return result;
        }
    }
}
