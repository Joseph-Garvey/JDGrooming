using JDGrooming.Classes;
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
                switch (UIView.Child)
                {
                    case BookClient client:
                        ChangeProgress(1);
                        break;
                    case BookDog dog:
                        ChangeProgress(2);
                        break;
                    case BookService service:
                        ChangeProgress(3);
                        break;
                }
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
        private BookService serviceView;
        public BookService ServiceView
        {
            get { return serviceView; }
            set
            {
                if (serviceView == value) return;
                serviceView = value;
                this.NotifyPropertyChanged("ServiceView");
            }
        }
        private BookTime timeView;
        public BookTime TimeView
        {
            get { return timeView; }
            set
            {
                if (timeView == value) return;
                timeView = value;
                this.NotifyPropertyChanged("TimeView");
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
                    ShowDogBooking(int.Parse(client.SelectedItem[0].ToString()));
                    break;
                case BookDog dog:
                    ShowServices(int.Parse(dog.SelectedItem[0].ToString()), dog.ClientID, dog.SelectedItem[1].ToString());
                    break;
                case BookService service:
                    ShowTimes(service.DogID, service.ClientID, service.SelectedService, service.DogName);
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
                case BookService service:
                    ShowDogBooking(service.ClientID);
                    break;
                case BookTime time:
                    ShowServices(time.DogID, time.ClientID, time.Dogname);
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
            catch { MessageBox.Show("An error has occurred.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="ClientID"></param>
        private void ShowDogBooking(int ClientID)
        {
            try
            {
                DogView = new BookDog(ClientID);
                View = DogView;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select a client from the list.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Displays the service selection view
        /// </summary>
        /// <param name="DogID">ID belonging to Dog</param>
        private void ShowServices(int DogID, int ClientID, String DogName)
        {
            try
            {
                ServiceView = new BookService(DogID, ClientID, DogName);
                View = ServiceView;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select a dog from the list.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="DogID"></param>
        /// <param name="ClientID"></param>
        /// <param name="GroomingOption"></param>
        private void ShowTimes(int DogID, int ClientID, Service GroomingOption, String DogName)
        {
            try
            {
                TimeView = new BookTime(ClientID, DogID, GroomingOption, DogName);
                View = TimeView;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select an option from the list.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeProgress(int id) // maybe make it into a template of a progress bar in future
        {
            if(id != 1) SetImage(img_1, "1_Blank.png");
            else { SetImage(img_1, "1_Filled.png"); }
            if (id != 2) SetImage(img_2, "2_Blank.png");
            else { SetImage(img_2, "2_Filled.png"); }
            if (id != 3) SetImage(img_3, "3_Blank.png");
            else { SetImage(img_3, "3_Filled.png"); }
            if (id != 4) SetImage(img_4, "4_Blank.png");
            else { SetImage(img_4, "4_Filled.png"); }
        }
        private void SetImage(Image image, String source)
        {
            Uri uri = new Uri("pack://siteoforigin:,,,/Icons/Numbers/" +source);
            image.Source = new BitmapImage(uri);
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
