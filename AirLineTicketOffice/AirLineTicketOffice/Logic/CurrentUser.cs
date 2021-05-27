using System.ComponentModel;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Annotations;

namespace AirLineTicketOffice.Logic
{
    public static class CurrentUser
    {
        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void OnStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        private static string name="";
        private static string surname="";
        private static int account_ID=-1;
        private static bool isAdmin = false;

        public static bool IsAdmin
        {
            get => isAdmin;
            set
            {
                isAdmin = value;
                OnStaticPropertyChanged("IsAdmin");
            }
        }

        public static int AccountId
        {
            get => account_ID;
            set
            {
                account_ID = value;
                OnStaticPropertyChanged("AccountId");
            }
        }

        public static string Name
        {
            get => name;
            set
            {
                name = value;
                OnStaticPropertyChanged("Name");
            }
        }

        public static string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnStaticPropertyChanged("Surname");
            }
        }
    }
}