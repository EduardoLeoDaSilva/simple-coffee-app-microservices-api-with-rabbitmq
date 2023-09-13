using CoffeeOnDemandSolution.Common.Entities;

namespace CoffeeOnDemandSolution.StoresService.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }



        public void DecreaseStock()
        {
            this.Stock -= 1;
        }

        public void IncreaseStock()
        {
            this.Stock -= 1;
        }

    }
}
