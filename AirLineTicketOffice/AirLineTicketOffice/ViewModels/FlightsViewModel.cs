using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AirLineTicketOffice.Logic;
using AirLineTicketOffice.Model;
using AirLineTicketOffice.View;

namespace AirLineTicketOffice.ViewModels
{
   public partial class FlightsViewModel: IDataErrorInfo
    {
        private DateTime departure_day=DateTime.Now;
        private DateTime arrival_day;
        private string selected_service = "Economy";
        private string selected_departure_city;
        private string selected_arrival_city;
        private FlightVariant selectedFlight;
        private Airline selectedAirline;
        private Filter filter = new Filter();
        private DateTime dateToday = DateTime.Now;
        private MainSearch _mainSearch = new MainSearch();
        private ObservableCollection<FlightVariant> _flightVariants;
        private ObservableCollection<FlightVariant> oldState;
        private ObservableCollection<Ticket> tickets;
        private ObservableCollection<Place> _places;
        private ObservableCollection<Flight> _flights;
        private ObservableCollection<DateFlight> _dateFlights;
        private ObservableCollection<Service_class_info> _serviceClassInfos;
        private IDataErrorInfo _dataErrorInfoImplemetation;
        private int maxPrice;


        #region AdminVariables

        private string _newCompany_Name="";
        private string _newDepartureCity="";
        private string _newArrivalCity="";
        private int _newDepartureIndex;
        private int _newArrivalIndex;
        private int _newDepartureHour;
        private int _newDepartureMinutes;
        private int _newArrivalHour;
        private int _newArrivalMinutes;
        private int _count_economy_places;
        private int _count_business_places;
        private string _priceEconomyPlace;
        private string _priceBusinessPlace;
        private DateTime? _canceledFlightDate=DateTime.Today;
        private ObservableCollection<Ticket> _badTickets= new ObservableCollection<Ticket>();

        public ObservableCollection<Ticket> BadTickets
        {
            get => _badTickets;
            set
            {
                _badTickets = value;
                OnPropertyChanged();
            }
        }

        public DateTime? CanceledFlightDate
        {
            get => _canceledFlightDate;
            set
            {
                _canceledFlightDate = value;
                OnPropertyChanged();
            }
        }

        public string PriceEconomyPlace
        {
            get => _priceEconomyPlace;
            set
            {
                _priceEconomyPlace = value;
                OnPropertyChanged();
            }
        }

        public string PriceBusinessPlace
        {
            get => _priceBusinessPlace;
            set
            {
                _priceBusinessPlace = value;
                OnPropertyChanged();
            }
        }

        public string NewCompanyName
        {
            get => _newCompany_Name;
            set
            {
                _newCompany_Name = value;
                OnPropertyChanged();
            }
        }

        public string NewDepartureCity
        {
            get => _newDepartureCity;
            set
            {
                _newDepartureCity = value;
                OnPropertyChanged();
            }
        }

        public string NewArrivalCity
        {
            get => _newArrivalCity;
            set
            {
                _newArrivalCity = value;
                OnPropertyChanged();
            }
        }

        public int NewDepartureIndex
        {
            get => _newDepartureIndex;
            set
            {
                _newDepartureIndex = value;
                OnPropertyChanged();
            }
        }

        public int NewArrivalIndex
        {
            get => _newArrivalIndex;
            set
            {
                _newArrivalIndex = value;
                OnPropertyChanged();
            }
        }

        public int NewDepartureHour
        {
            get => _newDepartureHour;
            set
            {
                _newDepartureHour = value;
                OnPropertyChanged();
            }
        }

        public int NewDepartureMinutes
        {
            get => _newDepartureMinutes;
            set
            {
                _newDepartureMinutes = value;
                OnPropertyChanged();
            }
        }

        public int NewArrivalHour
        {
            get => _newArrivalHour;
            set
            {
                _newArrivalHour = value;
                OnPropertyChanged();
            }
        }

        public int NewArrivalMinutes
        {
            get => _newArrivalMinutes;
            set
            {
                _newArrivalMinutes = value;
                OnPropertyChanged();
            }
        }

        public int CountEconomyPlaces
        {
            get => _count_economy_places;
            set
            {
                _count_economy_places = value;
                OnPropertyChanged();
            }
        }

        public int CountBusinessPlaces
        {
            get => _count_business_places;
            set
            {
                _count_business_places = value;
                OnPropertyChanged();
            }
        }

        #endregion
        
        private string departure_city = "";
        private string arrival_city = "";
        private DateTime departure_date=DateTime.Today;
        private int service_class = (int) Service_class.Economy;

        public string DepartureCity
        {
            get => departure_city;
            set
            {
                departure_city = value;
                OnPropertyChanged("DepartureCity");
            }
        }

        public string ArrivalCity
        {
            get => arrival_city;
            set
            {
                arrival_city = value;
                OnPropertyChanged("ArrivalCity");
            }
        }

        public DateTime DepartureDate
        {
            get => departure_date;
            set
            {
                departure_date = value;
                OnPropertyChanged("DepartureDate");
            }
        }

        public int ServiceClass
        {
            get => service_class;
            set
            {
                service_class = value;
                OnPropertyChanged("ServiceClass");
            }
        }

        public string Test { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error=String.Empty;
                Regex regex=new Regex(@"[0-9_=+~,./?!@#$:;%&*()]");
                Regex regex1 = new Regex("\\d{3,4}.\\d{0,2}");
                switch (columnName)
                { 
                    case "DepartureCity":
                        if (regex.IsMatch(DepartureCity))
                        {
                            error = "Данное поле не должно сожержать других символов кроме букв";
                        }
                        break;
                    case "ArrivalCity":
                        if (regex.IsMatch(ArrivalCity))
                        {
                            error = "Данное поле не должно сожержать других символов кроме букв";
                        }
                        break;
                    case "NewCompanyName":
                        if (regex.IsMatch(NewCompanyName))
                        {
                            error = "Данное поле не должно сожержать других символов кроме букв";
                        }
                        break;
                    case "NewDepartureCity":
                        if (regex.IsMatch(NewDepartureCity))
                        {
                            error = "Данное поле не должно сожержать других символов кроме букв";
                        }
                        break;
                    case "NewArrivalCity":
                        if (regex.IsMatch(NewArrivalCity))
                        {
                            error = "Данное поле не должно сожержать других символов кроме букв";
                        }
                        break;
                        
                   
                    case "PriceEconomyPlace":
                        if (!regex1.IsMatch(PriceEconomyPlace))
                        {
                            error = "Формат ввода 0-4_цифр.0-2_цифры";
                        }
                        break;
                    case "PriceBusinessPlace":
                        if (!regex1.IsMatch(PriceBusinessPlace))
                        {
                            error = "Формат ввода 0-4_цифр.0-2_цифры";
                        }
                        break;
                        

                    case "DepartureDate":
                        if (DepartureDate<DateTime.Today)
                        {
                            error = "Дата отправления не может быть раньше текущей";
                        }
                        break;
                    
                    
                    //TODO Validation
                }

                return error;
            }
        }

        public string Error => _dataErrorInfoImplemetation.Error; 

   public ObservableCollection<Service_class_info> ServiceInfo
        {
            get => _serviceClassInfos;
            set
            {
                _serviceClassInfos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DateFlight> DateFlights 
        {
            get => _dateFlights;
            set
            {
                _dateFlights = value;
                OnPropertyChanged();
            }
        }
        
        
        public ObservableCollection<Flight> AllFlights
        {
            get => _flights;
            set
            {
                _flights = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Ticket> Tickets
        {
            get => tickets;
            set
            {
                tickets = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Place> _Places
        {
            get => _places;
            set
            {
                _places = value;
                OnPropertyChanged();
            }
        }
        public int MaxPrice
        {
            get => maxPrice;
            set
            {
                maxPrice = value;
                OnPropertyChanged();
            }
        }

        public MainSearch MainSearch
        {
            get => _mainSearch;
            set => _mainSearch = value;
        }

        public DateTime DateToday
        {
            get
            {
                return dateToday;
            }

            set 
            {
                dateToday = value;
                OnPropertyChanged("DateToday");
            }
        }
        public Filter Filter
        {
            get => filter;
            set
            {
                filter = value;
                OnPropertyChanged("Filter");
            }
        }

        public FlightVariant SelectedFlight
        {
            get => selectedFlight;
            set
            {
                selectedFlight = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FlightVariant> Flights { get; set; }

        public ObservableCollection<FlightVariant> filteredFlights
        {
            get=>_flightVariants;
            set
            {
                _flightVariants = value;
                OnPropertyChanged();
            }
        }

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
            Airlines = MainWindow.db.Airlines.Local;
            AllFlights= MainWindow.db.Flights.Local;
            DateFlights = MainWindow.db.DateFlights.Local;
            ServiceInfo = MainWindow.db.Service_classes.Local;
            _Places = MainWindow.db.Places.Local;
            Tickets = MainWindow.db.Tickets.Local;

            
            BadTickets= new ObservableCollection<Ticket>(Tickets.Where(u => u.departure_date < DateTime.Today).Select(u => u));
            
            var group_places = MainWindow.db.Places.GroupBy(p=>new {p.Flight_ID,p.service_class,p.price});

            var result = from flight in MainWindow.db.Flights
                join airline in MainWindow.db.Airlines on flight.Airline_ID equals airline.Company_ID
                join flight_info in MainWindow.db.DateFlights on flight.Flight_ID equals flight_info.flight_id
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

            selectedFlight = new FlightVariant();
            
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
            
                var flights_with_default_filter = Flights.Where(
                    u => u.ServiceClass == selected_service).Select(u => u);

                filteredFlights = new ObservableCollection<FlightVariant>(flights_with_default_filter);

                //iterator.Reset();
            maxPrice = Convert.ToInt32(Flights.Max(u => u.Price));
        }
        
        
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
