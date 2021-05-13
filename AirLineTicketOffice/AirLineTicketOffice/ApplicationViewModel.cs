using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Model;

namespace AirLineTicketOffice
{
    public class ApplicationViewModel:INotifyPropertyChanged
    {
        private ModelContext db;
        private Airline selectedAirline;
        private Flight selectedFlight;
        private Place selectedPlace;
        private Canceled_flights selectedCanceled_Flight;
        private Service_class_info selectedServiceClass;
        private DateFlight selectedDateFlight;
        private Ticket selectedTicket;
        private Passenger selectedPassenger;
        private Account selectedAccount;

        public ApplicationViewModel()
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
            Flights = db.Flights.Local;
            Places = db.Places.Local;
            CanceledFlightsList = db.CanceledFlightsCollection.Local;
            ServiceClassInfos = db.Service_classes.Local;
            DateFlights = db.DateFlights.Local;
            Tickets = db.Tickets.Local;
            Passengers = db.Passengers.Local;
            Accounts = db.Accounts.Local;
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