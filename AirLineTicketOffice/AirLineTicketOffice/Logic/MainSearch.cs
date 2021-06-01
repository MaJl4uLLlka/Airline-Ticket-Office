using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Annotations;

namespace AirLineTicketOffice.Logic
{
    public class MainSearch:INotifyPropertyChanged,IDataErrorInfo
    {
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

        public MainSearch(){}
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                string error=String.Empty;
                switch (columnName)
                {
                    case "DepartureCity":
                        break;
                    case "ArrivalCity":
                        
                        break;
                    case "DepartureDate":
                        if (DepartureDate <= DateTime.Today)
                        {
                            error = "Дата отправления должна быть не раньше сегодняшнего дня";
                        }
                        break;
                }

                return error;
            }
        }

        public string Error {
            get { throw new NotImplementedException(); }
        }
    }
}