using System;
using System.Linq;
using System.Windows;
using AirLineTicketOffice.Logic;
using AirLineTicketOffice.Model;
using AirLineTicketOffice.View;
using MaterialDesignThemes.Wpf;

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
                               if (MainWindow.db.Accounts.Count()!=0)
                               {
                                   var users = Accounts.Where(u => u.login == SelectedAccount.login).Select(u=>u);
                                   Account user = new Account();
                                  
                                   if (users.Count() != 0)
                                   {
                                       user = users.First();

                                       if (String.CompareOrdinal(SelectedAccount.password.GetHashCode().ToString(),
                                           user.password) == 0)
                                       {
                                           if (String.CompareOrdinal("Yes", user.isAdmin) == 0)
                                           {
                                                ((MainWindow)Application.Current.MainWindow).ChangeContent(PageClass.AdminPage);
                                                ApplicationViewModel.isAuthorized = true;
                                                CurrentUser.Name = "Administrator";
                                               CurrentUser.IsAdmin = true;
                                                foreach (Window window in Application.Current.MainWindow.OwnedWindows)
                                                {
                                                    window.Close();
                                                }
                                                
                                           }
                                           else
                                           {
                                               ApplicationViewModel.isAuthorized = true;
                                               //CurrentUser.Name
                                               //CurrentUser.Surname
                                           }
                                       }
                                       else
                                       {
                                           MessageBox.Show("Неверный пароль");
                                       }
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