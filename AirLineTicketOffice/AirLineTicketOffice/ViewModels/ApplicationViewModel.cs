using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Logic;
using AirLineTicketOffice.Model;
using AirLineTicketOffice.View;

namespace AirLineTicketOffice.ViewModels
{
    public class ApplicationViewModel:INotifyPropertyChanged
    {
        private Airline selectedAirline;
        private Flight selectedFlight;
        private Place selectedPlace;
        private Canceled_flights selectedCanceled_Flight;
        private Service_class_info selectedServiceClass;
        private DateFlight selectedDateFlight;
        private Ticket selectedTicket;
        private Passenger selectedPassenger;
        private Account selectedAccount;
        public static bool isAuthorized = false;
        

        public ApplicationViewModel()
        {
            Airlines = MainWindow.db.Airlines.Local;
            Flights = MainWindow.db.Flights.Local;
            Places = MainWindow.db.Places.Local;
            CanceledFlightsList = MainWindow.db.CanceledFlightsCollection.Local;
            ServiceClassInfos = MainWindow.db.Service_classes.Local;
            DateFlights = MainWindow.db.DateFlights.Local;
            Tickets = MainWindow.db.Tickets.Local;
            Passengers = MainWindow.db.Passengers.Local;
            Accounts = MainWindow.db.Accounts.Local;
        }

        public ObservableCollection<Airline> Airlines { get; set; }
        public ObservableCollection<Flight> Flights { get; set; }
        public ObservableCollection<Place> Places { get; set; }
        public ObservableCollection<Canceled_flights> CanceledFlightsList { get; set; }
        public ObservableCollection<Service_class_info> ServiceClassInfos { get; set; }
        public ObservableCollection<DateFlight> DateFlights { get; set; }
        public ObservableCollection<Ticket> Tickets{ get; set; }
        public ObservableCollection<Passenger> Passengers{ get; set; }
        public ObservableCollection<Account> Accounts{ get; set; }

        public Airline SelectedAirline
        {
            get => selectedAirline;
            set
            {
                selectedAirline = value;
                OnPropertyChanged("SelectedAirline");
            }
        }

        public Flight SelectedFlight
        {
            get => selectedFlight;
            set
            {
                selectedFlight = value;
                OnPropertyChanged("SelectedFlight");
            }
        }

        public Place SelectedPlace
        {
            get => selectedPlace;
            set
            {
                selectedPlace = value;
                OnPropertyChanged("SelectedPlace");
            }
        }

        public Canceled_flights SelectedCanceledFlight
        {
            get => selectedCanceled_Flight;
            set
            {
                selectedCanceled_Flight = value;
                OnPropertyChanged("SelectedCanceledFlight");
            }
        }

        public Service_class_info SelectedServiceClass
        {
            get => selectedServiceClass;
            set
            {
                selectedServiceClass = value;
                OnPropertyChanged("SelectedServiceClass");
            }
        }

        public DateFlight SelectedDateFlight
        {
            get => selectedDateFlight;
            set
            {
                selectedDateFlight = value;
                OnPropertyChanged("SelectedDateFlight");
            }
        }

        public Ticket SelectedTicket
        {
            get => selectedTicket;
            set
            {
                selectedTicket = value;
                OnPropertyChanged("SelectedTicket");
            }
        }

        public Passenger SelectedPassenger
        {
            get => selectedPassenger;
            set
            {
                selectedPassenger = value;
                OnPropertyChanged("SelectedPassenger");
            }
        }

        public Account SelectedAccount
        {
            get => selectedAccount;
            set
            {
                selectedAccount = value;
                OnPropertyChanged("SelectedAccount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        
    }
}