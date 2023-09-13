using CoffeeOnDemandSolution.Common.Data.Repositories;
using CoffeeOnDemandSolution.Common.RabbitMQ;
using CoffeeOnDemandSolution.Common.Reponses;
using CoffeeOnDemandSolution.OrdersService.Data;
using CoffeeOnDemandSolution.OrdersService.Entities;
using Newtonsoft.Json;
using System.Text;

namespace CoffeeOnDemandSolution.OrdersService.Background
{
    public class MessageConsumer : BackgroundService
    {
        private readonly Subscriber<Order> _subscriber;
        private readonly Publisher _publisher;
        private readonly IServiceScopeFactory _scopeFactory;
        public MessageConsumer(Subscriber<Order> subscriber, IServiceScopeFactory scopeFactory, Publisher publisher)
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
            var order = JsonConvert.DeserializeObject<MessageModel>(jsonData);
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetService<BaseRepository<ApplicationContext, Order>>();

            var orderDb = await repo.GetById(order.OrderId);

            if (order.Status == true)
                orderDb.Status = PaymentStatus.Approved;
            else
                orderDb.Status = PaymentStatus.Cancelled;

            await repo.Update(orderDb);

        }

        public class MessageModel
        {
            public Guid OrderId { get; set; }
            public bool Status { get; set; }
            public Guid ProductId { get; set; }
            public string Message { get; set; }
        }
    }
}
