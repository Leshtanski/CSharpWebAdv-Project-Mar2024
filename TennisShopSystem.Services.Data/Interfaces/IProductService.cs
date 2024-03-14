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

        Task CreateAsync(ProductFormModel formModel, string sellerId);

        Task<AllProductsFilteredAndPagedServiceModel> AllAsync(AllProductQueryModel queryModel);

        Task<IEnumerable<ProductAllViewModel>> AllBySellerIdAsync(string sellerId);

        Task<IEnumerable<ProductAllViewModel>> AllByUserIdAsync(string userId);

        Task<ProductDetailsViewModel?> GetDetailsByIdAsync(string productId);
    }
}
