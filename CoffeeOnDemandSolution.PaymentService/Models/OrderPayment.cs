namespace CoffeeOnDemandSolution.PaymentService.Models
{
    public class OrderPayment
    {
        public Guid OrderId { get; set; }
        public PaymentType PaymentType { get; set; }
        public Guid ProductId { get; set; }
    }

    public enum PaymentType
    {
        CreditCard,
        PIX,
        Voucher
    }
}
