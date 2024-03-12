namespace TennisShopSystem.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TennisShopSystem.Web.ViewModels.Home;
    using TennisShopSystem.Web.ViewModels.Product;

    public interface IProductService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeProductsAsync();

        Task CreateAsync(ProductFormModel formModel, string sellerId);
    }
}
