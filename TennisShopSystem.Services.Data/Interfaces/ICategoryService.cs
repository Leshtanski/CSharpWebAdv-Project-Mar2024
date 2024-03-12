namespace TennisShopSystem.Services.Data.Interfaces
{
    using TennisShopSystem.Web.ViewModels.Category;

    public interface ICategoryService
    {
        Task<IEnumerable<ProductSelectCategoryFormModel>> AllCategoriesAsync();

        Task<bool> ExistsByIdAsync(int id);
    }
}
