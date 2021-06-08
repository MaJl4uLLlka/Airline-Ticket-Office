using System.Linq;
using System.Windows;
using AirLineTicketOffice.Logic;
using AirLineTicketOffice.Model;
using AirLineTicketOffice.View;

namespace AirLineTicketOffice.ViewModels
{
    public partial class ReviewsViewModel
    {
        private RelayCommand _publishReview;
        
        public RelayCommand PublishReview
        {
            get
            {
                return _publishReview??
                       (_publishReview = new RelayCommand(o =>
                           {
                               if (!ApplicationViewModel.isAuthorized)
                               {
                                   MessageBox.Show("Оставлять отзывы могут только авторизованные пользователи","",MessageBoxButton.OK, MessageBoxImage.Information);
                                   return;
                               }

                               Review newReview = new Review();

                               newReview.Text = ReviewText;
                               var currentPassenger = MainWindow.db.Passengers.First(u =>
                                   u.name == CurrentUser.Name && u.surname == CurrentUser.Surname);
                               newReview.Account= currentPassenger.Account;
                               MainWindow.db.Reviews.Add(newReview);
                               MainWindow.db.SaveChanges();
                           },
                           o=> ReviewText.Length!=0 && !CurrentUser.IsAdmin));
            }
        }
    }
}