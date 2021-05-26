using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AirLineTicketOffice.ViewModels;

namespace AirLineTicketOffice.View
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            DataContext = new UsersViewModel();
            Loaded += Authorization_Load;
            Closing += Authorization_Closing;
        }

        private void Authorization_Closing(object sender, CancelEventArgs e)
        {
            DialogResult = false;
            ((MainWindow) Application.Current.MainWindow).Effect = null;
        }

        private void Authorization_Load(object sender, RoutedEventArgs e)
        {
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 26;
            ((MainWindow) Application.Current.MainWindow).Effect = blurEffect;
        }
        
        
    }
}
