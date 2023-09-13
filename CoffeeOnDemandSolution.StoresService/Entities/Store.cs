using CoffeeOnDemandSolution.Common.Entities;

namespace CoffeeOnDemandSolution.StoresService.Entities
{
    public class Store : BaseEntity
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<Menu> Menus { get; set; }
    }


    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
        public string FullAddress { get; set; }
    }
}
