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
    /// Interaction logic for SearchBooking.xaml
    /// </summary>
    public partial class SearchBooking : UserControl
    {
        public App JDApp { get => ((App)Application.Current); }

        public object[] SelectedItem { get => data_Booking.SelectedItem; }

        public SearchBooking()
        {
            this.DataContext = this;
            InitializeComponent();
            data_Booking.dataview.Width = 750;
            data_Booking.dataview.MaxHeight = 400;
            data_Booking.dataview.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            data_Booking.DataList = JDApp.query.FillAppointmentTable();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object[] selectedrow = data_Booking.SelectedItem;
                string TransactionID = selectedrow[0].ToString();
                string DogID = selectedrow[2].ToString();
                DateTime time = DateTime.Parse(selectedrow[4].ToString());
                JDApp.query.DeleteBooking(TransactionID, DogID, time);
                data_Booking.DataList = JDApp.query.FillAppointmentTable();
                data_Booking.dataview.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Select an item to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch { }
        }
    }
}
