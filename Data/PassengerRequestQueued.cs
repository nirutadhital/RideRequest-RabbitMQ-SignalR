using MediatR;
using RideRequestService.Models;

namespace RideRequestService.Data
{
    public class PassengerRequestQueued : INotification
    {
        public PassengerRequest PassengerRequest { get; }

        public PassengerRequestQueued(PassengerRequest passengerRequest)
        {
            PassengerRequest = passengerRequest;
        }
    }
}
