namespace CoffeeOnDemandSolution.StoresService.DTOs
{
    public class MenuDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid>? ProductIds { get; set; }

        public List<Guid>? StoreIds { get; set; }
    }
}
