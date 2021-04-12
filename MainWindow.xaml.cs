using JDGrooming.Classes;
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
        private BookingView Booking { get; set; }

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
            try
            {
                if (Dock_Window.Children.Contains(Booking))
                {
                    if(Booking.View is BookTime)
                    {
                        Booking.TimeView.BlockExit();
                    }  
                }
            }
            catch (AllergyException) { return; }
            // try user control v window
            // grid v viewbox v stackpanel
            switch (((Button)sender).Name)
            {
                case "btnAddClient":
                    RemoveBooking();
                    View.Child = new ClientRegistration();
                    break;
                case "btnAddDog":
                    RemoveBooking();
                    View.Child = new DogRegistration();
                    break;
                case "btnUpdateDog":
                    RemoveBooking();
                    View.Child = new UpdateDog();
                    break;
                case "btnUpdateClient":
                    RemoveBooking();
                    View.Child = new UpdateClient();
                    break;
                case "btnEmailTest":
                    RemoveBooking();
                    View.Child = new _2FA_Test();
                    break;
                case "btnBooking":
                    RemoveBooking();
                    View.Child = null;
                    Booking = new BookingView();
                    Dock_Window.Children.Add(Booking);
                    break;
                case "btnStaffManagement":
                    RemoveBooking();
                    View.Child = new StaffManagement();
                    break;
                case "btnBookingSearch":
                    RemoveBooking();
                    View.Child = new SearchBooking();
                    break;
            }
        }
        private void RemoveBooking()
        {
            if (Dock_Window.Children.Contains(Booking)) { Dock_Window.Children.Remove(Booking); }
        }
        #endregion
    }
}
