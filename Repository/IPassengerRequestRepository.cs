using RideRequestService.Data;
using RideRequestService.Models;

namespace RideRequestService.Repository
{
    public interface IPassengerRequestRepository
    {
        Task<PassengerRequest> AddPassengerRequest(PassengerRequest request);
    }
}
