using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AirLineTicketOffice.ViewModels
{
    public class Filter
    {
        public List<String> Companies;
        public Nullable<TimeSpan> Departure_time;
        public Nullable<TimeSpan> Arrival_time;
        public Nullable<decimal>  Price;
        public Nullable<TimeSpan> Duration; // TODO may be destroyed

        public List<string> Companies1
        {
            get => Companies;
            set => Companies = value;
        }

        public TimeSpan? DepartureTime
        {
            get => Departure_time;
            set => Departure_time = value;
        }

        public TimeSpan? ArrivalTime
        {
            get => Arrival_time;
            set => Arrival_time = value;
        }

        public decimal? Price1
        {
            get => Price;
            set => Price = value;
        }

        
    }
}