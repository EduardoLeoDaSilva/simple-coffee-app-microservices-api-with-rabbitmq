namespace CoffeeOnDemandSolution.StoresService.DTOs
{
    public class StoreDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string FullAddress { get; set; }
        public List<Guid>? MenuIds { get; set; }
    }
}
