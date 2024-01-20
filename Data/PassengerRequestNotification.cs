using MediatR;
using RideRequestService.Models;

namespace RideRequestService.Data
{
    public class PassengerRequestNotification : INotification
    {
        public PassengerRequest? PassengerRequest { get; set; }
    }
}
