using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Model;

namespace AirLineTicketOffice
{
    public class ApplicationViewModel:INotifyPropertyChanged
    {
        private Airline selectedAirline;
 
        public ObservableCollection<Airline> Airlines { get; set; }
        
        public Airline SelectedAirline
        {
            get { return selectedAirline; }
            set
            {
                selectedAirline = value;
                OnPropertyChanged("SelectedAirline");
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