using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Annotations;
using AirLineTicketOffice.Logic;
using AirLineTicketOffice.Model;
using AirLineTicketOffice.View;

namespace AirLineTicketOffice.ViewModels
{
    public partial class UsersViewModel : INotifyPropertyChanged
    {
        private Passenger selectedPassenger;
        private Account selectedAccount;
        private RegistrationClass _registration;

        public RegistrationClass Registration
        {
            get => _registration;
            set
            {
                if (Equals(value, _registration)) return;
                _registration = value;
                OnPropertyChanged();
            }
        }

        public Passenger SelectedPassenger
        {
            get => selectedPassenger;
            set => selectedPassenger = value;
        }

        public Account SelectedAccount
        {
            get => selectedAccount;
            set => selectedAccount = value;
        }

        public ObservableCollection<Passenger> Passengers { get; set; }
        public ObservableCollection<Account> Accounts { get; set; }

        public UsersViewModel()
        {
            selectedAccount = new Account();
            selectedPassenger = new Passenger();
            selectedAccount.login = "";
            selectedAccount.password = "";
            Passengers = MainWindow.db.Passengers.Local;
            Accounts = MainWindow.db.Accounts.Local;
            _registration = new RegistrationClass();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}