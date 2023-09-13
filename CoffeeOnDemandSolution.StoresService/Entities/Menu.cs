using CoffeeOnDemandSolution.Common.Entities;

namespace CoffeeOnDemandSolution.StoresService.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }


        public List<Store> Stores { get; set; }


    }
}
