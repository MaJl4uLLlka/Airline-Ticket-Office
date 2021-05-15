using System.Windows.Controls;
using AirLineTicketOffice.ViewModels;

namespace AirLineTicketOffice.View
{
    /// <summary>
    /// Логика взаимодействия для Tickets_page.xaml
    /// </summary>
    public partial class Tickets_page : Page
    {
        public Tickets_page()
        {
            InitializeComponent();
            DataContext = new FlightsViewModel();
        }
    }
}
