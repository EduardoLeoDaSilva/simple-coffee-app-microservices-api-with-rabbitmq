using CoffeeOnDemandSolution.StoresService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeOnDemandSolution.StoresService.Data.ConfigMappings
{
    public class MenuMapping : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.DeletedAt == null);
            builder.Property(x => x.Name);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdateAt);
            builder.Property(x => x.DeletedAt);
            builder.HasMany(x => x.Stores).WithMany(x => x.Menus);
            builder.HasMany(x => x.Products);
        }
    }
}
