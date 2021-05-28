using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Annotations;

namespace AirLineTicketOffice.Logic
{
    public class RegistrationClass:INotifyPropertyChanged
    {
        private string login="";
        private string password="";
        private string repeatPassword="";
        private string isAdmin="No";
        private string name="";
        private string surname="";
        private string passport_ID="";
        private DateTime? birthdate=DateTime.Today;
        private DateTime? validity_period=DateTime.Today;

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

        public string RepeatPassword
        {
            get => repeatPassword;
            set
            {
                if (value == repeatPassword) return;
                repeatPassword = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}