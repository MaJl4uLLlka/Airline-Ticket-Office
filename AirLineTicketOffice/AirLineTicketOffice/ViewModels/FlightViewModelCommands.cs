using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AirLineTicketOffice.Logic;
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
                           o=>Filter.DepartureTimeSelected !=false ||
                              Filter.ArrivalTimeSelected!=false || Filter.PriceSelected!=false));
            }
        }
        
        public RelayCommand BuyCommand
        {
            get
            {
                return buy ??
                       (buy = new RelayCommand(o =>
                           {
                               if (!ApplicationViewModel.isAuthorized)
                               {
                                   Authorization authWindow = new Authorization();
                                   authWindow.Owner = Application.Current.MainWindow;
                                   if (authWindow.ShowDialog() == true)
                                   {
                                       
                                   }
                               }
                               else
                               {
                                   if(CurrentUser.IsAdmin)
                                   {
                                       //TODO ChangeThis
                                       MessageBox.Show("А по своим шалавливым ручкам получить хочешь?");
                                   }
                                   else
                                   {
                                       //TODO Tickets_binding
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
                        switch (MainSearch.ServiceClass)
                        {
                            case (int)Economy:
                                serviceClass = "Economy";
                                break;
                            
                            case (int)Business:
                                serviceClass = "Business";
                                break;
                        }

                        var foundFlights = Flights.Where(obj =>
                            obj.DepartureCity == MainSearch.DepartureCity &&
                            obj.ArrivalCity == MainSearch.ArrivalCity &&
                            obj.DepartureDay == MainSearch.DepartureDate.DayOfWeek.ToString() &&
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
        (o => (MainSearch.DepartureCity != "") && (MainSearch.ArrivalCity != "")
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
        
        
        
    }
}