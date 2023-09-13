using CoffeeOnDemandSolution.StoresService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CoffeeOnDemandSolution.StoresService.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationContext)));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
