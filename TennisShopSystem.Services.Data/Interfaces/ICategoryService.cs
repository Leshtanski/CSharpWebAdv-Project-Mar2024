namespace TennisShopSystem.Services.Data.Interfaces
{
    using TennisShopSystem.DataTransferObjects.Category;

    public interface ICategoryService
    {
        Task<IEnumerable<ProductSelectCategoryFormDto>> AllCategoriesAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<string>> AllCategoryNamesAsync();
    }
}
