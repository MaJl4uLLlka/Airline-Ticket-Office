using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AirLineTicketOffice.Annotations;
using AirLineTicketOffice.Logic;
using AirLineTicketOffice.Model;
using AirLineTicketOffice.View;

namespace AirLineTicketOffice.ViewModels
{
    public partial class UsersViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private Passenger selectedPassenger;
        private Account selectedAccount;
        private IDataErrorInfo _dataErrorInfoImplemetation;
        private string login="";
        private string password="";
        private string repeatPassword="";
        private string isAdmin="No";
        private string name="";
        private string surname="";
        private string passport_ID="";
        private DateTime? birthdate=DateTime.Today;
        private DateTime? validity_period=DateTime.Today;
        private DateTime? _dateToday;

        public DateTime? DateToday
        {
            get => _dateToday;
            set
            {
                if (Nullable.Equals(value, _dateToday)) return;
                _dateToday = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get => login;
            set
            {
                if (value == login) return;
                login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (value == password) return;
                password = value;
                OnPropertyChanged();
            }
        }
        
        public string IsAdmin
        {
            get => isAdmin;
            set
            {
                if (value == isAdmin) return;
                isAdmin = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                if (value == surname) return;
                surname = value;
                OnPropertyChanged();
            }
        }

        public string PassportId
        {
            get => passport_ID;
            set
            {
                if (value == passport_ID) return;
                passport_ID = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Birthdate
        {
            get => birthdate;
            set
            {
                if (value.Equals(birthdate)) return;
                birthdate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? ValidityPeriod
        {
            get => validity_period;
            set
            {
                if (value.Equals(validity_period)) return;
                validity_period = value;
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
        }

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
                Regex forbiddenSymbols = new Regex(@"[<>?#@!~`,.%&]");
                switch (columnName)
                {
                    case "Login":
                        if (forbiddenSymbols.IsMatch(Login))
                        {
                            error = "Логин не должен содержать запрещенных символов";
                        }
                        break;
                    case "Password":
                        if (forbiddenSymbols.IsMatch(Password))
                        {
                            error = "Пароль не должен содержать запрещенных символов";
                        }
                        break;
                    case "Name":
                        Regex onlyText = new Regex(@"[a-zA-ZА-Яа-я]{2,20}");
                        if (!onlyText.IsMatch(Name))
                        {
                            error = "Имя должно содержать только буквы и должно быть не короче 2 символов";
                        }
                        break;
                    case "Surname":
                        Regex onlyText1 = new Regex(@"[a-zA-ZА-Яа-я]{3,20}");
                        if (!onlyText1.IsMatch(Name))
                        {
                            error = "Фамилия должна содержать только буквы и должна быть не короче 3 символов";
                        }
                        
                        break;
                    case "PassportId":
                        Regex passport = new Regex(@"KH\d{7}");
                        if (!passport.IsMatch(PassportId))
                        {
                            error = "Формат: КНxxxxxxx";
                        }
                        break;
                }
                
                return error;
            }
        }

        public string Error => _dataErrorInfoImplemetation.Error; 
    }
}