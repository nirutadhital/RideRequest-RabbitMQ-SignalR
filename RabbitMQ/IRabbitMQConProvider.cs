using RabbitMQ.Client;

namespace RideRequestService.RabbitMQ
{
    public interface IRabbitMQConProvider
    {
        IConnection GetConnection();
    }
}
