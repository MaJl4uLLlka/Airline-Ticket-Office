using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AirLineTicketOffice.Annotations;
using AirLineTicketOffice.Logic;
using AirLineTicketOffice.Model;
using AirLineTicketOffice.View;

namespace AirLineTicketOffice.ViewModels
{
    partial class ReviewsViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<ReviewInfo> _reviews;
        private string _reviewText="";

        public string ReviewText
        {
            get => _reviewText;
            set
            {
                if (value == _reviewText) return;
                _reviewText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ReviewInfo> Reviews
        {
            get => _reviews;
            set
            {
                if (Equals(value, _reviews)) return;
                _reviews = value;
                OnPropertyChanged();
            }
        }

        public ReviewsViewModel()
        {
            Reviews = new ObservableCollection<ReviewInfo>();
            var allReviews = MainWindow.db.Reviews.Local.OrderByDescending(u=>u.ID);
            foreach (var review in allReviews)
            {
                ReviewInfo reviewInfo = new ReviewInfo();

                reviewInfo.Text = review.Text;
                reviewInfo.Fullname = review.Account.Passengers.First().name + " " +
                                      review.Account.Passengers.First().surname;
                
                Reviews.Add(reviewInfo);
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
