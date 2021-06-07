using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Annotations;

namespace AirLineTicketOffice.Logic
{
    public class MyTicket:INotifyPropertyChanged
    {
        private string _airline;
        private string _departureCity;
        private string _arrivalCity;
        private string _departureDate;
        private TimeSpan? _departureTime;
        private string _arrivalDate;
        private TimeSpan? _arrivalTime;
        private string _serviceClass;
        private int? _place;

        public int? Place
        {
            get => _place;
            set
            {
                if (value == _place) return;
                _place = value;
                OnPropertyChanged();
            }
        }

        public string ServiceClass
        {
            get => _serviceClass;
            set
            {
                if (value == _serviceClass) return;
                _serviceClass = value;
                OnPropertyChanged();
            }
        }

        public string Airline
        {
            get => _airline;
            set
            {
                if (value == _airline) return;
                _airline = value;
                OnPropertyChanged();
            }
        }

        public string DepartureCity
        {
            get => _departureCity;
            set
            {
                if (value == _departureCity) return;
                _departureCity = value;
                OnPropertyChanged();
            }
        }

        public string ArrivalCity
        {
            get => _arrivalCity;
            set
            {
                if (value == _arrivalCity) return;
                _arrivalCity = value;
                OnPropertyChanged();
            }
        }

        public string DepartureDate
        {
            get => _departureDate;
            set
            {
                if (value.Equals(_departureDate)) return;
                _departureDate = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan? DepartureTime
        {
            get => _departureTime;
            set
            {
                if (value.Equals(_departureTime)) return;
                _departureTime = value;
                OnPropertyChanged();
            }
        }

        public string ArrivalDate
        {
            get => _arrivalDate;
            set
            {
                if (value.Equals(_arrivalDate)) return;
                _arrivalDate = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan? ArrivalTime
        {
            get => _arrivalTime;
            set
            {
                if (value.Equals(_arrivalTime)) return;
                _arrivalTime = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}