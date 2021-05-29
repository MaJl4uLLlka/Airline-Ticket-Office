using System.Windows;
using System.Windows.Media.Effects;
using AirLineTicketOffice.View;
using AirLineTicketOffice.ViewModels;

namespace AirLineTicketOffice.Logic
{
    public class BindingProxy : Freezable
    {
        private RelayCommand exitCommand;

        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ??
                       (exitCommand = new RelayCommand(o =>
                           {
                               BlurEffect blurEffect = new BlurEffect();
                               blurEffect.Radius = 30;
                               ((MainWindow) Application.Current.MainWindow).Effect = blurEffect;
                               ApplicationViewModel.isAuthorized = false;
                               CurrentUser.Name = "";
                               CurrentUser.Surname = "";
                               CurrentUser.IsAdmin = false;
                               ((MainWindow)Application.Current.MainWindow).ChangeContent(PageClass.TicketPage);
                               ((MainWindow) Application.Current.MainWindow).Effect = null;
                           },
                           o=>ApplicationViewModel.isAuthorized==true
                       ));
            }
        }
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
    }
}