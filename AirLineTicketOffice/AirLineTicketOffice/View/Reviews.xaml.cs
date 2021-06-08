using System.Windows.Controls;
using AirLineTicketOffice.ViewModels;

namespace AirLineTicketOffice.View
{
    public partial class Reviews : Page
    {
        public Reviews()
        {
            InitializeComponent();
            DataContext = new ReviewsViewModel();
        }
    }
}