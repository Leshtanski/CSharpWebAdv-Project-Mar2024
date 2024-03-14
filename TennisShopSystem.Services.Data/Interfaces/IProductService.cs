namespace TennisShopSystem.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TennisShopSystem.Services.Data.Models.Product;
    using TennisShopSystem.Web.ViewModels.Home;
    using TennisShopSystem.Web.ViewModels.Product;

    public interface IProductService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeProductsAsync();

        Task<string> CreateAndReturnIdAsync(ProductFormModel formModel, string sellerId);

        Task<AllProductsFilteredAndPagedServiceModel> AllAsync(AllProductQueryModel queryModel);

        Task<IEnumerable<ProductAllViewModel>> AllBySellerIdAsync(string sellerId);

        Task<IEnumerable<ProductAllViewModel>> AllByUserIdAsync(string userId);

        Task<bool> ExistsByIdAsync(string productId);

        Task<ProductDetailsViewModel> GetDetailsByIdAsync(string productId);

        Task<ProductFormModel> GetProductForEditByIdAsync(string productId);

        Task<bool> IsSellerWithIdOwnerOfProductWithIdAsync(string productId, string sellerId);

        Task EditProductByIdAndFormModelAsync(string productId, ProductFormModel formModel);

        Task<ProductPreDeleteDetailsViewModel> GetProductForDeleteByIdAsync(string productId);

        Task DeleteProductByIdAsync(string productId);
    }
}
