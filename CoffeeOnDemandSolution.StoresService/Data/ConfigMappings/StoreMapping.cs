using CoffeeOnDemandSolution.StoresService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace CoffeeOnDemandSolution.StoresService.Data.ConfigMappings
{
    public class StoreMapping : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.DeletedAt == null);
            builder.Property(x => x.Name);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdateAt);
            builder.Property(x => x.DeletedAt);
            builder.HasMany(x => x.Menus).WithMany(x => x.Stores);
            builder.Property(x => x.Address).HasConversion(x => JsonConvert.SerializeObject(x), x => JsonConvert.DeserializeObject<Address>(x));
        }
    }
}
