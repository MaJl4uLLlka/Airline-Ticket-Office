using System.Windows.Controls;
using AirLineTicketOffice.ViewModels;

namespace AirLineTicketOffice.View
{
    public partial class LogIn : Page
    {
        public LogIn()
        {
            InitializeComponent();
            DataContext = new UsersViewModel();
        }
    }
}