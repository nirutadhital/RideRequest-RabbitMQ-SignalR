using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace RideRequestService.RabbitMQ
{
    public class RabbitMQConProvider : IRabbitMQConProvider
    {
        private readonly IConnection _connection;

        public RabbitMQConProvider(IConfiguration configuration)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost"
                    // HostName = configuration["RabbitMQ:HostName"],
                    // UserName = configuration["RabbitMQ:UserName"],
                    // Password = configuration["RabbitMQ:Password"],
                    // VirtualHost = configuration["RabbitMQ:VirtualHost"],
                    // Port = int.Parse(configuration["RabbitMQ:Port"]),
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to RabbitMQ: {ex.Message}");
                throw;
            }
        }

        public IConnection GetConnection() => _connection;
    }
}
