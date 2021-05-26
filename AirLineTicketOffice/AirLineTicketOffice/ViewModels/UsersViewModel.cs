using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Annotations;
using AirLineTicketOffice.Model;

namespace AirLineTicketOffice.ViewModels
{
    public partial class UsersViewModel : INotifyPropertyChanged
    {
        private ModelContext db;
        private Passenger selectedPassenger;
        private Account selectedAccount;

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
            db = new ModelContext();
            
            db.Accounts.Load();
            db.Passengers.Load();

            Passengers = db.Passengers.Local;
            Accounts = db.Accounts.Local;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}