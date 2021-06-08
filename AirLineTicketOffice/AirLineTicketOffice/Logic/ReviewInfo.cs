using System.ComponentModel;
using System.Runtime.CompilerServices;
using AirLineTicketOffice.Annotations;

namespace AirLineTicketOffice.Logic
{
    public class ReviewInfo:INotifyPropertyChanged
    {
        private string _fullname;
        private string _text;

        public string Fullname
        {
            get => _fullname;
            set
            {
                if (value == _fullname) return;
                _fullname = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                if (value == _text) return;
                _text = value;
                OnPropertyChanged();
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