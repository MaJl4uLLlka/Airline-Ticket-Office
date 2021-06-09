using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using AirLineTicketOffice.Logic;
using AirLineTicketOffice.Model;
using AirLineTicketOffice.View;
using static AirLineTicketOffice.Logic.Service_class;

namespace AirLineTicketOffice.ViewModels
{
    public partial class FlightsViewModel:INotifyPropertyChanged
    {
        private RelayCommand search;
        private RelayCommand getFilterCommand;
        private RelayCommand buy;
        private RelayCommand resetCommand;
        private RelayCommand addFlight;
        private RelayCommand removeFLight;
        private RelayCommand cancelFlight;
        
        
        public RelayCommand CancelFlightCommand
        {
            get
            {
                //TODO check
                return cancelFlight ??
                       (cancelFlight = new RelayCommand(o =>
                           {
                               FlightVariant selectedFlight = o as FlightVariant;
                               var currentFlight = AllFlights.Where(u =>
                                   u.Airline.company_name == selectedFlight.CompanyName &&
                                   u.departure_city == selectedFlight.DepartureCity &&
                                   u.arrival_city == selectedFlight.ArrivalCity).Select(u => u).First();

                               Canceled_flights canceledFlight = new Canceled_flights();
                               canceledFlight.departure_date=CanceledFlightDate;
                               canceledFlight.departure_time = selectedFlight.DepartureTime;
                               canceledFlight.Flight = currentFlight;

                               MainWindow.db.CanceledFlightsCollection.Add(canceledFlight);
                               MainWindow.db.SaveChanges();
                               
                               SelectedFlight = null;
                               CanceledFlightDate=DateTime.Today;
                               //currentFlight.Canceled_flights
                           },
                           o => o != null
                       ));
            }
        }

        public RelayCommand RemoveFlightCommand
        {
            get
            { //TODO Check
                return removeFLight ??
                       (removeFLight = new RelayCommand(o =>
                       {
                           FlightVariant flight=o as FlightVariant;
                           if (flight!=null)
                           {
                               filteredFlights.Remove(flight);
                               Flights.Remove(flight);

                               var selectedFlight=AllFlights.Where(u => u.Airline.company_name == flight.CompanyName &&
                                                                        u.departure_city == flight.DepartureCity &&
                                                                        u.arrival_city == flight.ArrivalCity).Select(u=>u).First();
                               
                               var deletingDateFlight = selectedFlight.DateFlights.Where(u => u.departure_day == flight.DepartureDay &&
                                                                     u.departure_time == flight.DepartureTime)
                                   .Select(u => u).First();

                               MainWindow.db.DateFlights.Remove(deletingDateFlight);
                               MainWindow.db.SaveChanges();

                               SelectedFlight = null;
                           }
                       },
                        o=>Flights.Count()!=0));
            }
        }
        
        public RelayCommand AddFlightCommand
        {
            get
            {
                return addFlight ??
                       (addFlight = new RelayCommand(o =>
                       {
                           FlightVariant newFlightVariant = new FlightVariant();

                           Airline currentAirline;
                           bool exist = false;

                           //Авиакомпания
                           if (Airlines.Any(u => u.company_name == NewCompanyName))
                           {
                               currentAirline = MainWindow.db.Airlines.First(u => u.company_name == NewCompanyName);
                               exist = true;
                           }
                           else
                           {
                               currentAirline = new Airline();
                               currentAirline.company_name = NewCompanyName;
                           }

                           Flight newFlight = new Flight();
                           newFlight.departure_city = NewDepartureCity;
                           newFlight.arrival_city = NewArrivalCity;

                           Service_class_info economyclassInfo = new Service_class_info
                           {
                               service_class = "Economy", count_places = CountEconomyPlaces
                           };

                           Service_class_info businessClassInfo = new Service_class_info
                           {
                               service_class = "Business", count_places = CountBusinessPlaces
                           };

                           //добавлена инфа о местах
                           newFlight.Service_class_info.Add(economyclassInfo);
                           newFlight.Service_class_info.Add(businessClassInfo);


                           string departureDay = "";
                           string arrivalDay = "";
                           switch (NewDepartureIndex)
                           {
                               case (int)DayOfWeak.Monday:
                                   departureDay = "Monday";
                                   break;
                               case (int)DayOfWeak.Tuesday:
                                   departureDay = "Tuesday";
                                   break;
                               case (int)DayOfWeak.Wednesday:
                                   departureDay = "Wednesday";
                                   break;
                               case (int)DayOfWeak.Thursday:
                                   departureDay = "Thursday";
                                   break;
                               case (int)DayOfWeak.Friday:
                                   departureDay = "Friday";
                                   break;
                               case (int)DayOfWeak.Saturday:
                                   departureDay = "Saturday";
                                   break;
                               case (int)DayOfWeak.Sunday:
                                   departureDay = "Sunday";
                                   break;
                           }
                           
                           switch (NewArrivalIndex)
                           {
                               case (int)DayOfWeak.Monday:
                                   arrivalDay = "Monday";
                                   break;
                               case (int)DayOfWeak.Tuesday:
                                   arrivalDay = "Tuesday";
                                   break;
                               case (int)DayOfWeak.Wednesday:
                                   arrivalDay = "Wednesday";
                                   break;
                               case (int)DayOfWeak.Thursday:
                                   arrivalDay = "Thursday";
                                   break;
                               case (int)DayOfWeak.Friday:
                                   arrivalDay = "Friday";
                                   break;
                               case (int)DayOfWeak.Saturday:
                                   arrivalDay = "Saturday";
                                   break;
                               case (int)DayOfWeak.Sunday:
                                   arrivalDay = "Sunday";
                                   break;
                           }

                           TimeSpan? departureTime = new TimeSpan(NewDepartureHour, NewDepartureMinutes, 0);
                           TimeSpan? arrivalTime = new TimeSpan(NewArrivalHour, NewArrivalMinutes ,0);
                           
                           DateFlight dateNewFlight = new DateFlight
                           {
                               departure_day = departureDay,
                               departure_time = departureTime,
                               arrival_day = arrivalDay,
                               arrival_time = arrivalTime
                           };
                           
                           //инфа о рейсе
                           newFlight.DateFlights.Add(dateNewFlight);
                           
                           //преобразование string к decimal 
                           decimal priceEconomyPlace =Decimal.Parse(PriceEconomyPlace,NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                           decimal priceBusinessPlace = Decimal.Parse(PriceBusinessPlace, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

                           if (priceEconomyPlace>priceBusinessPlace)
                           {
                               MessageBox.Show("Цена билета бизнес класса ниже, чем цена билета эконом класса","", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                               return;
                           }
                           //генерация мест
                           for (int i = 1; i <= economyclassInfo.count_places; i++)
                           {
                               newFlight.Places.Add(new Place
                               {
                                   place_number = i,
                                   service_class = economyclassInfo.service_class,
                                   price = priceEconomyPlace
                               });
                           }
                           
                           for (int i = 1; i <= businessClassInfo.count_places; i++)
                           {
                               newFlight.Places.Add(new Place
                               {
                                   place_number = i,
                                   service_class = businessClassInfo.service_class,
                                   price = priceBusinessPlace
                               });
                           }
                           
                           

                           if (!exist)
                           {
                               currentAirline.Flights.Add(newFlight);
                               MainWindow.db.Airlines.Add(currentAirline);
                           }
                           else
                           {
                               newFlight.Airline = currentAirline;
                               MainWindow.db.Flights.Add(newFlight);
                           }
                           MainWindow.db.SaveChanges();


                           newFlightVariant.DepartureCity = NewDepartureCity;
                           newFlightVariant.CompanyName = NewCompanyName;
                           newFlightVariant.ArrivalCity = NewArrivalCity;
                           newFlightVariant.DepartureDay = departureDay;
                           newFlightVariant.DepartureTime = departureTime;
                           newFlightVariant.ArrivalDay = arrivalDay;
                           newFlightVariant.ArrivalTime = arrivalTime;
                           newFlightVariant.ServiceClass = "Economy";
                           newFlightVariant.Price = priceEconomyPlace;

                           FlightVariant newBusinessFlightVariant = new FlightVariant
                           {
                               DepartureCity = NewDepartureCity,
                               CompanyName = NewCompanyName,
                               ArrivalCity = NewArrivalCity,
                               DepartureDay = departureDay,
                               DepartureTime = departureTime,
                               ArrivalDay = arrivalDay,
                               ArrivalTime = arrivalTime,
                               ServiceClass = "Economy",
                               Price = priceBusinessPlace
                           };
                           
                           Flights.Insert(0,newFlightVariant);
                           Flights.Insert(0, newBusinessFlightVariant);
                           
                           //сброс переменных
                           NewCompanyName = "";
                           NewDepartureCity ="";
                           NewArrivalCity = "";
                           NewDepartureIndex = 0;
                           NewArrivalIndex = 0;
                           NewDepartureHour = 0;
                           NewDepartureMinutes = 0;
                           NewArrivalHour = 0;
                           NewArrivalMinutes = 0;
                           CountEconomyPlaces = 0;
                           CountBusinessPlaces = 0;
                           PriceEconomyPlace = "";
                           PriceBusinessPlace = "";
                       },
                           o=> NewCompanyName!="" && NewDepartureCity!="" && NewArrivalCity!="" &&
                               CountEconomyPlaces>0 && CountBusinessPlaces>0 && PriceEconomyPlace!="" &&
                               PriceBusinessPlace!=""));
            }
        }
        
        public RelayCommand ResetCommand
        {
            get
            {
                return resetCommand ??
                       (resetCommand = new RelayCommand(o =>
                       {
                           Filter=new Filter();
                           filteredFlights = oldState;
                       },
                           o=> Filter.DepartureTimeSelected || Filter.ArrivalTimeSelected || Filter.PriceSelected));
            }
        }
        
        public RelayCommand GetFilterCommand
        {
            get
            {
                return getFilterCommand ??
                       (getFilterCommand = new RelayCommand(o =>
                       {
                           ApplyFilter();
                       },
                           o=>Filter.DepartureTimeSelected  ||
                              Filter.ArrivalTimeSelected || Filter.PriceSelected));
            }
        }
        
        public RelayCommand BuyCommand 
        {
            get
            {
                return buy ??
                       (buy = new RelayCommand(o =>
                           {
                               ToggleButton currentButton = o as ToggleButton;
                               if (!ApplicationViewModel.isAuthorized)
                               {
                                   Authorization authWindow = new Authorization();
                                   authWindow.Owner = Application.Current.MainWindow;
                                   if (authWindow.ShowDialog() == true)
                                   {
                                       
                                   }
                                   currentButton.IsChecked = false;
                                   return;
                                   
                               }

                               if(!ApplicationViewModel.isAuthorized)
                               {
                                   currentButton.IsChecked = false;
                                   return;
                               }
                              
                               if(CurrentUser.IsAdmin)
                               {
                                   MessageBox.Show("Для покупки войдите в свой личный акканут","",MessageBoxButton.OK, MessageBoxImage.Information);
                               }
                               else 
                               {
                                   try
                                   {
                                       Passenger currentPassenger = MainWindow.db.Passengers.First(u=>u.name==CurrentUser.Name && u.surname==CurrentUser.Surname);
                                       Airline currentAirline = Airlines
                                           .First(u => SelectedFlight.CompanyName==u.company_name);
                                   
                                   
                                       Flight currentFlight=currentAirline.Flights.First(
                                           u=>u.departure_city==SelectedFlight.DepartureCity && u.arrival_city==SelectedFlight.ArrivalCity);

                                       List<Place> places = currentFlight.Places.ToList();

                                       var TicketsAndPlaces = places.Join(Tickets,
                                           p => p.Place_ID,
                                           t => t.Place_ID,
                                           ((place, ticket) =>
                                               new
                                               {
                                                   ticket.Ticket_ID,
                                                   ticket.Place_ID,
                                                   ticket.Place,
                                                   ticket.Passenger,
                                                   ticket.departure_date,
                                                   ticket.departure_time,
                                                   ticket.isCanceled,
                                                   ticket.isExpired
                                               }));

                                       //генерация билетов, если их нет
                                       if ( TicketsAndPlaces.Count() == 0 || !TicketsAndPlaces.Any(u=>u.departure_date==DepartureDay))
                                       {
                                           List<Ticket> tickets = new List<Ticket>();
                                           for (int i = 0; i < places.Count(); i++)
                                           {
                                               tickets.Add(new Ticket
                                               {
                                                   Place = places[i],
                                                   Passenger = null,
                                                   departure_date = DepartureDay,
                                                   departure_time = SelectedFlight.DepartureTime,
                                                   isCanceled = "No",
                                                   isExpired = "No"
                                               });
                                           }
                                       
                                           tickets[0].Passenger = currentPassenger;
                                       
                                           MainWindow.db.Tickets.AddRange(tickets);
                                           MainWindow.db.SaveChanges();
                                       }
                                       else //если билеты уже сгенерированны
                                       {
                                           var freeTicketID=TicketsAndPlaces.Where(u => u.Passenger == null &&
                                               u.departure_date==DepartureDay).Select(u=>u.Ticket_ID).First();
                                           Ticket freeTicket=MainWindow.db.Tickets.First(u=>u.Ticket_ID==freeTicketID);
                                           currentPassenger.Tickets.Add(freeTicket);
                                           MainWindow.db.SaveChanges();
                                       }

                                       currentButton.IsChecked = false;
                                       MessageBox.Show("\u2714  Билет успешно приобретён");
                                   }
                                   catch
                                   {
                                       MessageBox.Show("К сожалению, в данный момент выбранный рейс недоступен","",MessageBoxButton.OK, MessageBoxImage.Information);
                                       currentButton.IsChecked = false;
                                   }
                                   
                               }
                         
                           }
                       ));
            }
        }
        
        public RelayCommand Search 
        {
            get
            {
                return search ?? (search = new RelayCommand(o =>
                    {
                        string serviceClass = "";
                        switch (ServiceClass)
                        {
                            case (int)Economy:
                                serviceClass = "Economy";
                                break;
                            
                            case (int)Business:
                                serviceClass = "Business";
                                break;
                        }

                        var foundFlights = Flights.Where(obj =>
                            obj.DepartureCity == DepartureCity &&
                            obj.ArrivalCity == ArrivalCity &&
                            obj.DepartureDay == DepartureDate.DayOfWeek.ToString() &&
                            obj.ServiceClass == serviceClass).Select(obj => obj);

                        filteredFlights.Clear();
                        
                        var iterator = foundFlights.GetEnumerator();

                        while (iterator.MoveNext())
                        {
                            filteredFlights.Add( new FlightVariant
                            {
                                CompanyName = iterator.Current.CompanyName,
                                DepartureCity = iterator.Current.DepartureCity,
                                ArrivalCity = iterator.Current.ArrivalCity,
                                DepartureDay = iterator.Current.DepartureDay,
                                DepartureTime = iterator.Current.DepartureTime,
                                ArrivalDay = iterator.Current.ArrivalDay,
                                ArrivalTime = iterator.Current.ArrivalTime,
                                ServiceClass = iterator.Current.ServiceClass,
                                Price = iterator.Current.Price
                            });
                        }
                    },
        (o => (DepartureCity != "") && (ArrivalCity != "")
                )));
            }
        }
        
        
        public void ApplyFilter()
        {
            ObservableCollection<FlightVariant> filteringResult = new ObservableCollection<FlightVariant>();
            List<FlightVariant> flightVariants = filteredFlights.ToList();
            
            
            if (Filter.DepartureTimeSelected)
            {
                var filterByDepartureTime = flightVariants.Where(u=>u.DepartureTime< new TimeSpan(Filter.Departure_time,0 ,0)).Select(u=>u);
                flightVariants= new List<FlightVariant>();
                flightVariants.AddRange(filterByDepartureTime);
            }

            if (Filter.ArrivalTimeSelected)
            {
                var filterByArrivalTime = flightVariants.Where(u=>u.ArrivalTime< new TimeSpan(Filter.Arrival_time,0 ,0));
                flightVariants= new List<FlightVariant>();
                flightVariants.AddRange(filterByArrivalTime);
            }

            if (Filter.PriceSelected)
            {
                var filterByPrice = flightVariants.Where(u=>u.Price< Filter.Price1);
                flightVariants= new List<FlightVariant>();
                flightVariants.AddRange(filterByPrice);
            }

            
            filteringResult= new ObservableCollection<FlightVariant>(flightVariants);
            oldState = filteredFlights;
            filteredFlights = filteringResult;
        }


        void CheckPassengerTicket(Passenger passenger)
        {
            
        }
        
        
    }
}