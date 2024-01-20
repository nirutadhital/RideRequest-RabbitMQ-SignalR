using Microsoft.EntityFrameworkCore;
using RideRequestService.Models;
using System.Collections.Generic;

namespace RideRequestService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<PassengerRequest> PassengerRequests { get; set; }

    }
}
