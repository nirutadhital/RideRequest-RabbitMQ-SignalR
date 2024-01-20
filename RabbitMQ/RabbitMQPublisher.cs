using Newtonsoft.Json;
using System.Text;
using RabbitMQ.Client;
using Microsoft.AspNet.SignalR.Json;


namespace RideRequestService.RabbitMQ
{
    public class RabbitMQPublisher
    {
        private readonly IRabbitMQConProvider _connectionProvider;

        public RabbitMQPublisher(IRabbitMQConProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void PublishMessage<T>(T message, string queueName)
        {
            using (var channel = _connectionProvider.GetConnection().CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                // var json = JsonSerializer.Serialize(message);
                // var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }
    }
}
