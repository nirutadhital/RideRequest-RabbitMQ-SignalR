using Microsoft.AspNetCore.SignalR;
using RideRequestService.Consumer;
using RideRequestService.Data;

namespace RideRequestService.SignalRHub
{
    public class PassengerRequestHub : Hub
    {
        private readonly RabbitMQConsumer _rabbitMQConsumer;

        public PassengerRequestHub(RabbitMQConsumer rabbitMQConsumer)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
        }
        public async Task SendPassengerRequestNotification(PassengerRequestNotification notification)
        {
            await Clients.All.SendAsync("ReceivePassengerRequest", notification);
        }
        public override async Task OnConnectedAsync()
        {
            _rabbitMQConsumer.ConsumeQueue("PassengerRequestQueue");
            await base.OnConnectedAsync();
        }
    }
}
