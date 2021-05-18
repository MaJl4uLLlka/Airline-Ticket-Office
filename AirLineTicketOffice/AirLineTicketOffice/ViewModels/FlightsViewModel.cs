using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Model;

namespace AirLineTicketOffice.ViewModels
{
    class FlightsViewModel:INotifyPropertyChanged
    {
        private ModelContext db;
        private DateTime departure_day=DateTime.Now;
        private DateTime arrival_day;
        private string selected_service = "Economy";
        private string selected_departure_city;
        private string selected_arrival_city;
        private FlightVariant selectedFlight;
        private Airline selectedAirline;

        public FlightVariant SelectedFlight
        {
            get => selectedFlight;
            set
            {
                selectedFlight = value;
                OnPropertyChanged("SelectedFlight");
            }
        }

        public ObservableCollection<FlightVariant> Flights { get; set; }
        public ObservableCollection<FlightVariant> filteredFlights { get; set; }

        public ObservableCollection<Airline> Airlines { get; set; }
        public Airline SelectedAirline
        {
            get => selectedAirline;
            set
            {
                selectedAirline = value;
                OnPropertyChanged("SelectedAirline");
            }
        }

        public DateTime ArrivalDay
        {
            get => arrival_day;
            set
            {
                arrival_day = value;
                OnPropertyChanged("ArrivalDay");
            }
        }

        public string SelectedService
        {
            get => selected_service;
            set
            {
                selected_service = value;
                OnPropertyChanged("SelectedService");
            }
        }

        public string SelectedDepartureCity
        {
            get => selected_departure_city;
            set
            {
                selected_departure_city = value;
                OnPropertyChanged("SelectedDepartureCity");
            }
        }

        public string SelectedArrivalCity
        {
            get => selected_arrival_city;
            set
            {
                selected_arrival_city = value;
                OnPropertyChanged("SelectedArrivalCity");
            }
        }
        
        public DateTime DepartureDay
        {
            get => departure_day;
            set
            {
                departure_day = value;
                OnPropertyChanged("DepartureDay");
            }
        }
        
        public FlightsViewModel()
        {
            db = new ModelContext();
            
            db.Airlines.Load();
            db.Flights.Load();
            db.Places.Load();
            db.CanceledFlightsCollection.Load();
            db.Service_classes.Load();
            db.DateFlights.Load();
            db.Tickets.Load();
            db.Passengers.Load();
            db.Accounts.Load();

            Airlines = db.Airlines.Local;

            var group_places = db.Places.GroupBy(p=>new {p.Flight_ID,p.service_class,p.price});

            var result = from flight in db.Flights
                join airline in db.Airlines on flight.Airline_ID equals airline.Company_ID
                join flight_info in db.DateFlights on flight.Flight_ID equals flight_info.flight_id
                join group_place in group_places on flight.Flight_ID equals group_place.Key.Flight_ID
                select new 
                {
                    Company=airline.company_name,
                    Depart_city=flight.departure_city,
                    Arrival_city=flight.arrival_city,
                    Depart_day=flight_info.departure_day,
                    Depart_time=flight_info.departure_time,
                    Arrival_day=flight_info.arrival_day,
                    Arrival_time=flight_info.arrival_time,
                    Serv_class=group_place.Key.service_class,
                    Price=group_place.Key.price
                };
            
            Flights = new ObservableCollection<FlightVariant>();

            var iterator = result.GetEnumerator();

            while (iterator.MoveNext())
            {
                Flights.Add( new FlightVariant
                {
                    CompanyName = iterator.Current.Company,
                    DepartureCity = iterator.Current.Depart_city,
                    ArrivalCity = iterator.Current.Arrival_city,
                    DepartureDay = iterator.Current.Depart_day,
                    DepartureTime = iterator.Current.Depart_time,
                    ArrivalDay = iterator.Current.Arrival_day,
                    ArrivalTime = iterator.Current.Arrival_time,
                    ServiceClass = iterator.Current.Serv_class,
                    Price = iterator.Current.Price
                });
            }
            
            //iterator.Reset();

            var flights_with_default_filter = Flights.Where(
                u => u.ServiceClass == selected_service &&
                     u.DepartureDay == departure_day.DayOfWeek.ToString() &&
                     u.DepartureTime > departure_day.TimeOfDay).Select(u => u);

            filteredFlights = new ObservableCollection<FlightVariant>(flights_with_default_filter);
        }
        
        
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
