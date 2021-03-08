using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //btnUpdateDog2.Items = new List<MenuItem> { new MenuItem("Update"), new MenuItem("Register") };
        }

        #region Button Events
        /// <summary>
        /// Opens Hamburger Menu
        /// </summary>
        private void BtnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            BtnCloseMenu.Visibility = Visibility.Visible;
            BtnOpenMenu.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Closes Hamburger Menu
        /// </summary>
        private void BtnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            BtnCloseMenu.Visibility = Visibility.Collapsed;
            BtnOpenMenu.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Changes Active Display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // try user control v window
            // grid v viewbox v stackpanel
            switch (((Button)sender).Name)
            {
                case "btnAddClient":
                    View.Child = new ClientRegistration();
                    break;
                case "btnAddDog":
                    View.Child = new DogRegistration();
                    break;
                case "btnUpdateDog":
                    View.Child = new UpdateDog();
                    break;
                case "btnUpdateClient":
                    View.Child = new UpdateClient();
                    break;
                case "btnEmailTest":
                    View.Child = new _2FA_Test();
                    break;
                case "btnBooking":
                    View.Child = null;
                    BookingView b = new BookingView();
                    Dock_Window.Children.Add(b);
                    break;
            }
        }
        #endregion
    }
}
