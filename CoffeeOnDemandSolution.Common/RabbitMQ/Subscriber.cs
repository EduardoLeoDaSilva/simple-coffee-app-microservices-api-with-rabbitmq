using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeOnDemandSolution.Common.RabbitMQ
{
    public class Subscriber<QueueName>
    {
        private readonly IConnection _connnection;
        private readonly IModel _channel;
        private readonly string _queueName;
        public event Action<BasicDeliverEventArgs> OnMessage;
        private readonly IConfiguration _configuration;
        public Subscriber(IConfiguration configuration)
        {
            _configuration = configuration;

            _connnection = new ConnectionFactory { HostName = _configuration["RabbitMQ:Hostname"], Port = int.Parse(_configuration["RabbitMQ:Port"]), UserName = _configuration["RabbitMQ:Username"], Password = _configuration["RabbitMQ:Password"] }.CreateConnection();
            _channel = _connnection.CreateModel();
            _queueName = typeof(QueueName).Name;

        }

        public async void Subscribe()
        {
            await Task.Delay(5000);
            _channel.ExchangeDeclare("main", ExchangeType.Direct, true, false);

            _channel.QueueDeclare(_queueName, durable: true);
            _channel.QueueBind(_queueName, "main", _queueName);
            EventingBasicConsumer basicConsumer = new EventingBasicConsumer(_channel);

            basicConsumer.Received += BasicConsumer_Received;

            _channel.BasicConsume(_queueName, true, basicConsumer);

        }

        private void BasicConsumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            OnMessage.Invoke(e);
        }
    }
}
