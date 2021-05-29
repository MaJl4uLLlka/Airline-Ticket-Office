using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AirLineTicketOffice.ViewModels
{
    public class FlightVariant:INotifyPropertyChanged
    {
        private string company_name;
        private string departure_city;
        private string arrival_city;
        private string departure_day=DateTime.Today.DayOfWeek.ToString();
        private Nullable<TimeSpan> departure_time;
        private string arrival_day;
        private Nullable<TimeSpan> arrival_time;
        private string service_class="Economy";
        private Nullable<decimal> price;

        public string CompanyName
        {
            get => company_name;
             set
            {
                company_name = value;
                OnPropertyChanged("CompanyName");
            }
        }

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

        public string DepartureDay
        {
            get => departure_day;
            set
            {
                departure_day = value;
                OnPropertyChanged("DepartureDay");
            }
        }

        public TimeSpan? DepartureTime
        {
            get => departure_time;
            set
            {
                departure_time = value;
                OnPropertyChanged("DepartureTime");
            }
        }

        public string ArrivalDay
        {
            get => arrival_day;
            set
            {
                arrival_day = value;
                OnPropertyChanged("ArrivalDay");
            }
        }

        public TimeSpan? ArrivalTime
        {
            get => arrival_time;
            set
            {
                arrival_time = value;
                OnPropertyChanged("ArrivalTime");
            }
        }

        public string ServiceClass
        {
            get => service_class;
            set
            {
                service_class = value;
                OnPropertyChanged("ServiceClass");
            }
        }

        public decimal? Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
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