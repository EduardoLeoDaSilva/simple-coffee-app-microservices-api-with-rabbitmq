using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeOnDemandSolution.Common.RabbitMQ
{
    public class Publisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _configuration;
        public Publisher(IConfiguration configuration)
        {
            _configuration = configuration;
            var tt = _configuration["RabbitMQ:Hostname"];
            _connection = new ConnectionFactory { HostName = _configuration["RabbitMQ:Hostname"], Port = int.Parse(_configuration["RabbitMQ:Port"]), UserName = _configuration["RabbitMQ:Username"], Password = _configuration["RabbitMQ:Password"] }.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("main", ExchangeType.Direct, true, false);
        }

        public void Publish<E>(E data, string routingKey)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var dataBytes = Encoding.UTF8.GetBytes(jsonData);
            _channel.BasicPublish("main", routingKey, body: dataBytes);
        }

    }
}
