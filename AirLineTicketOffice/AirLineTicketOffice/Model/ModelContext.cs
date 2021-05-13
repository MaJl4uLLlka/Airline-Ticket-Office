using System.Data.Entity;

namespace AirLineTicketOffice.Model
{
    class ModelContext : DbContext
    {
        public ModelContext():base("AirlineTicketOfficeEntities"){}
        
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Canceled_flights> CanceledFlightsCollection { get; set; }
        public DbSet<Service_class_info> Service_classes { get; set; }
        public DbSet<DateFlight> DateFlights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Account>   Accounts { get; set; }
    }
}