using System.Data.Entity;

namespace AirLineTicketOffice.Model
{
    public class ModelContext : DbContext
    {
        public ModelContext():base("AirlineTicketOfficeEntities1")
        {
            Airlines.Load();
            Flights.Load();
            Places.Load();
            CanceledFlightsCollection.Load();
            Service_classes.Load();
            DateFlights.Load();
            Tickets.Load();
            Passengers.Load();
            Accounts.Load();
        }
        
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