using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Annotations;
using AirLineTicketOffice.Logic;
using AirLineTicketOffice.Model;
using AirLineTicketOffice.View;

namespace AirLineTicketOffice.ViewModels
{
    public class MyTicketsViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<Passenger> _passengers;
        private ObservableCollection<Ticket> _tickets;

        public ObservableCollection<Ticket> Tickets
        {
            get => _tickets;
            set
            {
                if (Equals(value, _tickets)) return;
                _tickets = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Passenger> Passengers
        {
            get => _passengers;
            set
            {
                if (Equals(value, _passengers)) return;
                _passengers = value;
                OnPropertyChanged();
            }
        }

        public MyTicketsViewModel()
        {
            Passengers = MainWindow.db.Passengers.Local;
            Tickets = MainWindow.db.Tickets.Local;
            _badTickets = new ObservableCollection<MyTicket>();
            _myTickets = new ObservableCollection<MyTicket>();

            if (CurrentUser.IsAdmin)
            {
                List<Ticket> badTickets = Tickets.Where(u => u.departure_date < DateTime.Today).ToList();
                List<Place> badTicketsPlaces = new List<Place>();

            //
            foreach (var badTicket in badTickets)
            {
                badTicketsPlaces.Add(badTicket.Place);
            }
            
            //
            List<Flight> badFlights = new List<Flight>();

            foreach (var badTicketsPlace in badTicketsPlaces)
            {
                badFlights.Add(badTicketsPlace.Flight);
            }

            
            List<Airline> badAirlines = new List<Airline>();

            //
            foreach (var badFlight  in badFlights)
            {
                badAirlines.Add(badFlight.Airline);
            }
            
            //
            List<DateFlight> badDateFlights = new List<DateFlight>();
            
            for (int i = 0; i < badTickets.Count(); i++)
            {
                badDateFlights.Add(badFlights[i].DateFlights.First(u => u.departure_time == badTickets[i].departure_time));
            }
            
            for(int i =0; i<badTickets.Count();i++ )
            {
                _badTickets.Add(new MyTicket
                    {
                        DepartureDate = badTickets[i].departure_date?.ToShortDateString(),
                        DepartureTime = badTickets[i].departure_time,
                        DepartureCity = badFlights[i].departure_city,
                        ArrivalCity = badFlights[i].arrival_city,
                        ArrivalDate = badTickets[i].departure_date?.ToShortDateString(),
                        ArrivalTime = badDateFlights[i].arrival_time,
                        Place = badTicketsPlaces[i].place_number,
                        ServiceClass = badTicketsPlaces[i].service_class,
                        Airline = badAirlines[i].company_name
                    }
                );
            }
               
            }
            else
            {
                Passenger current_passenger = Passengers.First(u=>
                    u.name==CurrentUser.Name && u.surname==CurrentUser.Surname);
                List<Ticket> tickets = current_passenger.Tickets.ToList();
                List<Place> places = new List<Place>();

                foreach (var ticket in tickets)
                {
                    places.Add(ticket.Place);
                }
                
                List<Flight> flights = new List<Flight>();
                foreach (var place in places)
                {
                    flights.Add(place.Flight);
                }

                List<Airline> airlines = new List<Airline>();
                foreach (var flight in flights)
                {
                    airlines.Add(flight.Airline);
                }
                
                List<DateFlight> dateFlights = new List<DateFlight>();
                for (int i = 0; i < tickets.Count(); i++)
                {
                    dateFlights.Add(flights[i].DateFlights.First(u => u.departure_time == tickets[i].departure_time));
                }
                
                for(int i =0; i<tickets.Count();i++ )
                {
                    _myTickets.Add(new MyTicket
                        {
                            DepartureDate = tickets[i].departure_date?.ToShortDateString(),
                            DepartureTime = tickets[i].departure_time,
                            DepartureCity = flights[i].departure_city,
                            ArrivalCity = flights[i].arrival_city,
                            ArrivalDate = tickets[i].departure_date?.ToShortDateString(),
                            ArrivalTime = dateFlights[i].arrival_time,
                            Place = places[i].place_number,
                            ServiceClass = places[i].service_class,
                            Airline = airlines[i].company_name
                        }
                    );
                }
                
            }
           


            
           

            

           

            
        }

        private ObservableCollection<MyTicket> _badTickets;
        private ObservableCollection<MyTicket> _myTickets;

        public ObservableCollection<MyTicket> BadTickets
        {
            get => _badTickets;
            set
            {
                if (Equals(value, _badTickets)) return;
                _badTickets = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MyTicket> MyTickets
        {
            get => _myTickets;
            set
            {
                if (Equals(value, _myTickets)) return;
                _myTickets = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}