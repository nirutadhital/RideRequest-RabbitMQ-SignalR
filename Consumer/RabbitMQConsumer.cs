using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RideRequestService.Data;
using RideRequestService.Models;
using RideRequestService.RabbitMQ;
using System.Text;

namespace RideRequestService.Consumer
{
    public class RabbitMQConsumer
    {
        private readonly IRabbitMQConProvider _connectionProvider;
        private readonly IMediator _mediator;

        public RabbitMQConsumer(IRabbitMQConProvider connectionProvider, IMediator mediator)
        {
            _connectionProvider = connectionProvider;
            _mediator = mediator;
        }

        public void ConsumeQueue(string queueName)
        {
            using (var channel = _connectionProvider.GetConnection().CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = JsonConvert.DeserializeObject<PassengerRequest>(Encoding.UTF8.GetString(body));

                    _mediator.Publish(new PassengerRequestNotification { PassengerRequest = message });
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            }
        }
    }
}
