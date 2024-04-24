namespace TennisShopSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using TennisShopSystem.Data;
    using Interfaces;
    using TennisShopSystem.DataTransferObjects.Brand;

    public class BrandService : IBrandService
    {
        private readonly TennisShopDbContext dbContext;

        public BrandService(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> AllBrandNamesAsync()
        {
            IEnumerable<string> allBrandNames = await this.dbContext
                .Brands
                .Select(b => b.Name)
                .ToArrayAsync();

            return allBrandNames;
        }

        public async Task<IEnumerable<ProductSelectBrandFormDto>> AllBrandsAsync()
        {
            IEnumerable<ProductSelectBrandFormDto> allBrands = await this.dbContext
                .Brands
                .AsNoTracking()
                .Select(c => new ProductSelectBrandFormDto
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
