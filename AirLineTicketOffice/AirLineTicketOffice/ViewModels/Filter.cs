using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Annotations;

namespace AirLineTicketOffice.ViewModels
{
    public class Filter:INotifyPropertyChanged
    {
        public int Departure_time=0;
        public int Arrival_time=0;
        public int  Price=0;
        
        private bool departure_timeSelected = false;
        private bool arrival_timeSelected = false;
        private bool priceSelected = false;
        
        public bool DepartureTimeSelected
        {
            get => departure_timeSelected;
            set
            {
                departure_timeSelected = value;
                OnPropertyChanged();
            }
        }

        public bool ArrivalTimeSelected
        {
            get => arrival_timeSelected;
            set
            {
                arrival_timeSelected = value;
                OnPropertyChanged();
            }
        }

        public bool PriceSelected
        {
            get => priceSelected;
            set
            {
                priceSelected = value;
                OnPropertyChanged();
            }
        }
        

        public int DepartureTime
        {
            get => Departure_time;
            set
            {
                Departure_time = value;
                DepartureTimeSelected= true;
                OnPropertyChanged();
            }
        }

        public int ArrivalTime
        {
            get => Arrival_time;
            set
            {
                Arrival_time = value;
                ArrivalTimeSelected = true;
                OnPropertyChanged();
            }
        }

        public int Price1
        {
            get => Price;
            set
            {
                Price = value;
                PriceSelected = true;
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