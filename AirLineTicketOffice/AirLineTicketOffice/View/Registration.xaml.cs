using System.Windows.Controls;
using AirLineTicketOffice.ViewModels;

namespace AirLineTicketOffice.View
{
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
            DataContext = new UsersViewModel();
        }
    }
}