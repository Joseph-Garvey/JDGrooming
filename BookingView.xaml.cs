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
                    ShowServices(int.Parse(dog.SelectedItem[0].ToString()));
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
            catch { MessageBox.Show("An error has occurred.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
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
        /// <summary>
        /// Displays the service selection view
        /// </summary>
        /// <param name="ID">ID belonging to Dog</param>
        private void ShowServices(int ID)
        {
            try
            {
                ServiceView = new BookService(ID);
                View = ServiceView;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select a dog from the list.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
