using CoffeeOnDemandSolution.Common.Entities;

namespace CoffeeOnDemandSolution.OrdersService.Entities
{
    public class Order : BaseEntity
    {
        public string CustomerName { get; set; }
        public Guid StoreId { get; set; }
        public Guid ProductId { get; set; }
        public PaymentStatus Status { get; set; }
        public decimal Total { get; set; }
    }

    public enum PaymentStatus
    {
        Processing,
        Cancelled,
        Approved
    }
}
