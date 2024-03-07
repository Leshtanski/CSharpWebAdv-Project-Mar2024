namespace TennisShopSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Tennis Rackets"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Tennis Clothing"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Tennis Shoes"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 4,
                Name = "Tennis Bags"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 5,
                Name = "Tennis Balls"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 6,
                Name = "Tennis Strings"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 7,
                Name = "Other"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
