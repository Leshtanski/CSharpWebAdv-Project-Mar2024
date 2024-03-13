namespace TennisShopSystem.Web.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;
    using TennisShopSystem.Web.ViewModels.Product.Enums;

    using static TennisShopSystem.Common.GeneralApplicationConstants;

    public class AllProductQueryModel
    {
        public AllProductQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.ProductsPerPage = EntitiesPerPage;

            this.Categories = new HashSet<string>();
            this.Brands = new HashSet<string>();
            this.Products = new HashSet<ProductAllViewModel>();
        }

        public string? Category { get; set; }

        public string? Brand { get; set; }

        [Display(Name = "Search by keyword")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort By")]
        public ProductSorting ProductSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Show Products Per Page")]
        public int ProductsPerPage { get; set; }

        public int TotalProducts { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<ProductAllViewModel> Products { get; set; }
    }
}
