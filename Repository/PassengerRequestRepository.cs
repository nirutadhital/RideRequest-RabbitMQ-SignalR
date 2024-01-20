using RideRequestService.Data;
using RideRequestService.Models;

namespace RideRequestService.Repository
{
    public class PassengerRequestRepository : IPassengerRequestRepository
    {
        private readonly DataContext _dataContext;
        public PassengerRequestRepository(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public async Task<PassengerRequest> AddPassengerRequest(PassengerRequest request)
        {
            // Add logic to save the request to the database
            var result = _dataContext.PassengerRequests.Add(request);
            await _dataContext.SaveChangesAsync();
            return  result.Entity;
        }
    }
}
