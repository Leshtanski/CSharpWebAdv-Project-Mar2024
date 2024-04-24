namespace TennisShopSystem.DataTransferObjects.Product
{
    using System.ComponentModel.DataAnnotations;
    using TennisShopSystem.DataTransferObjects.Product.Enums;
    using static Common.GeneralApplicationConstants;

    public class AllProductQueryDto
    {
        public AllProductQueryDto()
        {
            CurrentPage = DefaultPage;
            ProductsPerPage = EntitiesPerPage;

            Categories = new HashSet<string>();
            Brands = new HashSet<string>();
            Products = new HashSet<ProductAllDto>();
        }

        public string? Category { get; set; }

        public string? Brand { get; set; }

        public string? SearchString { get; set; }

        public ProductSorting ProductSorting { get; set; }

        public int CurrentPage { get; set; }

        public int ProductsPerPage { get; set; }

        public int TotalProducts { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<ProductAllDto> Products { get; set; }
    }
}
