namespace TennisShopSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using TennisShopSystem.Data;
    using Interfaces;
    using TennisShopSystem.DataTransferObjects.Category;

    public class CategoryService : ICategoryService
    {
        private readonly TennisShopDbContext dbContext;

        public CategoryService(TennisShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductSelectCategoryFormDto>> AllCategoriesAsync()
        {
            IEnumerable<ProductSelectCategoryFormDto> allCategories = await this.dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new ProductSelectCategoryFormDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
        {
            IEnumerable<string> allCategoryNames = await this.dbContext
                .Categories
                .Select(c => c.Name)
                .ToArrayAsync();

            return allCategoryNames;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .Categories
                .AnyAsync(c => c.Id == id);

            return result;
        }
    }
}
