using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Annotations;

namespace AirLineTicketOffice.Logic
{
    public class FullUserInfo:INotifyPropertyChanged
    {
        private string name;
        private string surname;
        private DateTime birthdate;
        private DateTime validity_period;
        private string passport_ID;
        private string account_ID;
        private string login;
        private string password;
        private string isAdmin;


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}