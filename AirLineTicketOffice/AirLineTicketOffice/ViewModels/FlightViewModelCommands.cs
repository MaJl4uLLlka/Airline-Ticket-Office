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
        private RelayCommand addValue;

        public RelayCommand AddValue
        {
            get
            {
                return addValue ??
                       (addValue = new RelayCommand(o =>
                       {
                           var checkbox = o as CheckBox;
                           Filter.Companies1.Add(checkbox.Content.ToString());
                       }));
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

        public RelayCommand GetFilterCommand
        {
            get
            {
                return getFilterCommand ??
                       (getFilterCommand = new RelayCommand(o =>
                       {
                           if (Filter.CompaniesSelected)
                           {
                               string str = "";
                               foreach (var company in Filter.Companies1)
                               {
                                   str += company + '\n';
                                   MessageBox.Show(str);
                               }
                           }

                           if (Filter.DepartureTimeSelected)
                           {
                               MessageBox.Show(Filter.Departure_time.ToString());
                           }

                           if (Filter.ArrivalTimeSelected)
                           {
                               MessageBox.Show(Filter.Arrival_time.ToString());
                           }

                           if (Filter.PriceSelected)
                           {
                               MessageBox.Show(Filter.Price.ToString());
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