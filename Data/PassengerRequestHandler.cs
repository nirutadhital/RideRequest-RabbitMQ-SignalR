using MediatR;

namespace RideRequestService.Data
{
    public class PassengerRequestHandler : INotificationHandler<PassengerRequestNotification>
    {
        private readonly IMediator _mediator;

        public PassengerRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(PassengerRequestNotification notification, CancellationToken cancellationToken)
        {
            // Handle the notification, e.g., send to RabbitMQ
            await _mediator.Publish(new PassengerRequestQueued(notification.PassengerRequest));
        }
    }
}
