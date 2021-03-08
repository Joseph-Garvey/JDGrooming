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
            set
            {
                UIView.Child = value;
            }
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
        private BookDog dogView;
        public BookDog DogView
        {
            get { return dogView; }
            set
            {
                if (dogView == value) return;
                dogView = value;
                this.NotifyPropertyChanged("DogView");
            }
        }

        public BookingView()
        {
            InitializeComponent();
            ShowClientBooking();
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            switch (View)
            {
                case BookClient client:
                    object[] obj = client.SelectedItem; // fix null check
                    int ID = int.Parse(client.SelectedItem[0].ToString());
                    ShowDogBooking(ID);
                    break;
                case BookDog dog:

                    break;
            }
        }

        private void Previous(object sender, RoutedEventArgs e)
        {
            switch (View)
            {
                case BookDog dog:
                    ShowClientBooking();
                    break;
            }
        }

        private void ShowClientBooking()
        {
            try
            {
                ClientView = new BookClient();
                View = ClientView;
            }
            catch { }
        }
        private void ShowDogBooking(int ID)
        {
            try
            {
                DogView = new BookDog(ID);
                View = DogView;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select a client from the list.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region PropertyChanged Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
