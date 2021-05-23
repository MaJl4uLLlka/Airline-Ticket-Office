using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using AirLineTicketOffice.Logic;
using static AirLineTicketOffice.Logic.Service_class;

namespace AirLineTicketOffice.ViewModels
{
    public partial class FlightsViewModel:INotifyPropertyChanged
    {
        private RelayCommand search;
        private RelayCommand getFilterCommand;

        public RelayCommand GetFilterCommand
        {
            get
            {
                return getFilterCommand ??
                       (getFilterCommand = new RelayCommand(o =>
                       {
                           Filter newFilter = o as Filter;
                           if (newFilter != null)
                           {
                               Filter = newFilter;
                           }
                           
                       }));
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
            List<FlightVariant> flightVariants= new List<FlightVariant>();
            
            if (Filter.Companies != null)
            {
                for (int i = 0; i < Filter.Companies.Count; i++)
                {
                    var groupFlights=Flights.Where(u => u.CompanyName == Filter.Companies[i]).Select(u=> u);
                    flightVariants=flightVariants.Union(groupFlights).ToList();
                }
            }
        }
        
    }
}