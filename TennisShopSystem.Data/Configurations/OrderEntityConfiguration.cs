namespace TennisShopSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TennisShopSystem.Data.Models;

    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasOne(o => o.OrderDetails)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
