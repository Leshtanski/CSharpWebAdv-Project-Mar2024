namespace TennisShopSystem.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    using Configurations;
    using Models;
    
    public class TennisShopDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly bool seedDb;

        public TennisShopDbContext(DbContextOptions<TennisShopDbContext> options, bool seedDb = true)
            : base(options)
        {
            this.seedDb = seedDb;
        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Brand> Brands { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Seller> Sellers { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<OrderDetails> OrdersDetails { get; set; } = null!;

        public DbSet<OrderedItem> OrderedItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(TennisShopDbContext)) 
                                   ?? Assembly.GetExecutingAssembly();

            if (this.seedDb)
            {
                builder.ApplyConfigurationsFromAssembly(configAssembly);
            }
            else
            {
                builder.ApplyConfiguration(new ProductEntityConfiguration());
            }

            base.OnModelCreating(builder);
        }
    }
}
