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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for BookingView.xaml
    /// </summary>
    public partial class BookingView : UserControl, INotifyPropertyChanged
    {
        public UserControl View
        {
            get { return (UserControl)UIView.Child; }
            set { UIView.Child = value; }
        }
        private BookClient clientView;
        public BookClient ClientView
        {
            get { return clientView; }
            set
            {
                if (clientView == value) return;
                clientView = value;
                this.NotifyPropertyChanged("ClientView");
            }
        }

        public BookingView()
        {
            ClientView = new BookClient();
            View = ClientView;
            InitializeComponent();
        }

        private void Next(object sender, RoutedEventArgs e)
        {

        }

        #region PropertyChanged Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }

    public enum BookingStatus
    {
        Client,
        Dog,
        Option,
        Date
    }
}
