using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using AirLineTicketOffice.Logic;
using AirLineTicketOffice.Model;
using AirLineTicketOffice.ViewModels;

namespace AirLineTicketOffice.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ModelContext db = new ModelContext();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }

        public void ChangeContent(PageClass pageClass)
        {
            switch (pageClass)
            {
                case PageClass.AdminPage:
                    Frame.Source = new Uri("AdminPage.xaml",UriKind.Relative);
                    break;
                case PageClass.ReviewPage:
                    Frame.Source = new Uri("Reviews.xaml",UriKind.Relative);
                    break;
                case PageClass.TicketPage:
                    Frame.Source = new Uri("Tickets_page.xaml",UriKind.Relative);
                    break;
            }
        }
    }
}
