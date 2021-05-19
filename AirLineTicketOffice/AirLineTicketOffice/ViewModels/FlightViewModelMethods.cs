using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace AirLineTicketOffice.ViewModels
{
    public partial class FlightsViewModel:INotifyPropertyChanged
    {
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

            if ()
            {
                
            }
        }
    }
}