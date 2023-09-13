using CoffeeOnDemandSolution.Common.RabbitMQ;
using CoffeeOnDemandSolution.Common.Reponses;
using CoffeeOnDemandSolution.PaymentService.Models;
using CoffeeOnDemandSolution.PaymentService.Services;
using Newtonsoft.Json;
using System.Text;

namespace CoffeeOnDemandSolution.PaymentService.Background
{
    public class MessageConsumer : BackgroundService
    {
        private readonly Subscriber<OrderPayment> _subscriber;
        private readonly Publisher _publisher;
        private readonly IServiceScopeFactory _scopeFactory;
        public MessageConsumer(Subscriber<OrderPayment> subscriber, IServiceScopeFactory scopeFactory, Publisher publisher)
        {
            _subscriber = subscriber;
            _scopeFactory = scopeFactory;
            _publisher = publisher;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriber.Subscribe();

            _subscriber.OnMessage += _subscriber_OnMessage;
            return Task.CompletedTask;
        }

        private async void _subscriber_OnMessage(RabbitMQ.Client.Events.BasicDeliverEventArgs obj)
        {
            var jsonData = Encoding.UTF8.GetString(obj.Body.ToArray());
            var orderPayment = JsonConvert.DeserializeObject<OrderPayment>(jsonData);
            using var scope = _scopeFactory.CreateScope();
            var paymentService = scope.ServiceProvider.GetService<IPaymentService>();
            BaseResponse<bool> result = null;
            switch (orderPayment.PaymentType)
            {
                case PaymentType.CreditCard:
                     result = await paymentService.PayWithCreditCard();
                    break;
                case PaymentType.PIX:
                     result = await paymentService.PayWithPix();
                    break;
                case PaymentType.Voucher:
                     result = await paymentService.PayWithVoucher();
                    break;
                default:
                    break;

            }
            _publisher.Publish<dynamic>(new { OrderId = orderPayment.OrderId, Status = result.Success, ProductId = orderPayment.ProductId, Message = result.Message }, "Store");
            _publisher.Publish<dynamic>(new { OrderId = orderPayment.OrderId, Status = result.Success, ProductId = orderPayment.ProductId, Message = result.Message }, "Order");
        }
    }
}
