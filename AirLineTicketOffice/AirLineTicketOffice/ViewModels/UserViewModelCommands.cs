using System;
using System.Linq;
using System.Windows;
using AirLineTicketOffice.Logic;
using AirLineTicketOffice.View;

namespace AirLineTicketOffice.ViewModels
{
    public partial class UsersViewModel
    {
        private RelayCommand checkAuthentification;
        
        public RelayCommand CheckAuthentification
        {
            get
            {
                return checkAuthentification ??
                       (checkAuthentification = new RelayCommand(o =>
                           {
                               if (db.Accounts.Count()!=0)
                               {
                                   var user = Accounts.First(u => u.login == SelectedAccount.login);

                                   if (user != null)
                                   {

                                       if (String.CompareOrdinal(SelectedAccount.password.GetHashCode().ToString(),
                                           user.password) == 0)
                                       {
                                           if (String.CompareOrdinal("Yes", user.isAdmin) == 0)
                                           {
                                                ((MainWindow)Application.Current.MainWindow).ChangeContent(new AdminPage());
                                           }
                                       }
                                       else MessageBox.Show("Неверный пароль");
                                   }
                                   else MessageBox.Show("Неверный логин");
                               }
                               else
                               {
                                   MessageBox.Show("В данный момент авторизация недоступна");
                               }
                           }
                       ));
            }
        }
    }
}