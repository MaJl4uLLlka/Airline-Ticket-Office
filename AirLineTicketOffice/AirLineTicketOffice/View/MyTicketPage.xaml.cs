using System.Windows.Controls;
using AirLineTicketOffice.ViewModels;

namespace AirLineTicketOffice.View
{
    public partial class MyTicketPage : Page
    {
        public MyTicketPage()
        {
            InitializeComponent();
            DataContext = new MyTicketsViewModel();
        }
    }
}