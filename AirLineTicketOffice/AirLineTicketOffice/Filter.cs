using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AirLineTicketOffice
{
    public class Filter : INotifyPropertyChanged
    {
        public List<String> Companies;
        public Nullable<TimeSpan> Departure_time;
        public Nullable<TimeSpan> Arrival_time;
        public Nullable<decimal>  Price;
        public Nullable<TimeSpan> Duration; // TODO may be destroyed
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}