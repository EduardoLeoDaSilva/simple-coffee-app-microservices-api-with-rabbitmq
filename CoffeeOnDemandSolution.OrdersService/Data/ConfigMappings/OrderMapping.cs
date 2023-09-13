using CoffeeOnDemandSolution.OrdersService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeOnDemandSolution.OrdersService.Data.ConfigMappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.DeletedAt == null);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdateAt);
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.ProductId);
            builder.Property(x => x.CustomerName);
            builder.Property(x => x.Status);
            builder.Property(x => x.StoreId);
            builder.Property(x => x.Total);

        }
    }
}
