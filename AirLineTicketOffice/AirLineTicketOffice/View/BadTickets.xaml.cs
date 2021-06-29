using System.Windows.Controls;
using AirLineTicketOffice.ViewModels;

namespace AirLineTicketOffice.View
{
    public partial class BadTickets : Page
    {
        public BadTickets()
        {
            InitializeComponent();
            DataContext = new MyTicketsViewModel();
        }
    }
}