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
        public MyTicketsViewModel()
        {
            var current_account = MainWindow.db.Accounts.First(u =>
                u.Passengers.First() != null && u.Passengers.First().name == CurrentUser.Name &&
                u.Passengers.First().surname == CurrentUser.Surname);

            Passenger current_passenger = current_account.Passengers.First();

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
            
            List<>

            for (int i = 0; i < tickets.Count(); i++)
            {
                
            }

            _myTickets = new ObservableCollection<MyTicket>();

            for(int i =0; i<tickets.Count();i++ )
            {
                _myTickets.Add(new MyTicket
                    {
                        DepartureDate = tickets[i].departure_date,
                        DepartureTime = tickets[i].departure_time,
                        Place = places[i].place_number,
                        ServiceClass = places[i].service_class,
                        
                    }
                        );
            }
        }
        
        private ObservableCollection<MyTicket> _myTickets;

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