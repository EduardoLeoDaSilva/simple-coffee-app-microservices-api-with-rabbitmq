using CoffeeOnDemandSolution.Common.Data.Repositories;
using CoffeeOnDemandSolution.Common.RabbitMQ;
using CoffeeOnDemandSolution.StoresService.Data;
using CoffeeOnDemandSolution.StoresService.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CoffeeOnDemandSolution.StoresService.BackgroundServices
{
    public class MessageConsumer : BackgroundService
    {
        private readonly Subscriber<Store> _subscriber;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public MessageConsumer(IServiceScopeFactory serviceScopeFactory, Subscriber<Store> subscriber)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _subscriber = subscriber;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriber.Subscribe();
            _subscriber.OnMessage += _subscriber_OnMessage;
            return Task.CompletedTask;

        }

        private async void _subscriber_OnMessage(RabbitMQ.Client.Events.BasicDeliverEventArgs obj)
        {
            var stringData = Encoding.UTF8.GetString(obj.Body.ToArray());
            var jsonData = JsonConvert.DeserializeObject<MessageModel>(stringData);

            using var scope = _serviceScopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetService<BaseRepository<ApplicationContext, Product>>();

            var product = await repository.GetById(jsonData.ProductId);
            product.DecreaseStock();

            await repository.Commit();
        }

        public class MessageModel
        {
            public Guid OrderId { get; set; }
            public string Status { get; set; }
            public Guid ProductId { get; set; }
            public string Message { get; set; }
        }
    }
}
