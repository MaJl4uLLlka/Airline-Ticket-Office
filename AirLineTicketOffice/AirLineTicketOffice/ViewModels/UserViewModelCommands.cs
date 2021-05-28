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
        private RelayCommand registration;

        public RelayCommand _Registration
        {
            get
            {
                return registration??
                       (registration= new RelayCommand(o =>
                       {
                           Account account = new Account();
                           account.login = Registration.Login;
                           account.password = Registration.Password.GetHashCode().ToString();
                           account.isAdmin = Registration.IsAdmin;
                           
                           Passenger passenger = new Passenger();
                           passenger.name = Registration.Name;
                           passenger.surname = Registration.Surname;
                           passenger.birthdate = Registration.Birthdate;
                           passenger.Passport_ID = Registration.PassportId;
                           passenger.validity_period = Registration.ValidityPeriod;

                           MainWindow.db.Accounts.Add(account);
                           MainWindow.db.SaveChanges();
                           
                       }));
            }
        }

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
                                               var passengers = Passengers.Where(p => p.Account_ID == user.Account_ID);
                                               if (passengers.Count()!=0)
                                               {
                                                   var passenger = passengers.First();
                                                   CurrentUser.Name = passenger.name;
                                                   CurrentUser.Surname = passenger.surname;
                                                   ApplicationViewModel.isAuthorized = true;
                                               }
                                               
                                               foreach (Window window in Application.Current.MainWindow.OwnedWindows)
                                               {
                                                   window.Close();
                                               }
                                           }
                                       }
                                       else //TODO change "else" after this string 
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