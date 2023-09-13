using CoffeeOnDemandSolution.StoresService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeOnDemandSolution.StoresService.Data.ConfigMappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.DeletedAt == null);
            builder.Property(x => x.Name);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdateAt);
            builder.Property(x => x.DeletedAt);
            builder.Property(x => x.Description);
            builder.Property(x => x.Stock);
            builder.Property(x => x.Price);
        }
    }
}
