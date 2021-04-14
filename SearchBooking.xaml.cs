using JDGrooming.Classes;
using System;
using System.Collections.Generic;
using System.Data;
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
            data_Booking.dataview.MaxHeight = 400;
            data_Booking.dataview.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            //
            Search_DataGrid tmp = new Search_DataGrid();
            tmp.DataList = JDApp.query.FillAppointmentTable();
            //
            List<String> DogIDs = new List<String>();
            foreach(DataRow n in tmp.DataList.Table.Rows)
            {
                object[] o = n.ItemArray;
                if (!DogIDs.Contains(o[2].ToString()))
                {
                    DogIDs.Add(o[2].ToString());
                }
            }
            //foreach(DataRowView r in tmp.dataview.Items)
            //{
            //    object[] o = r.Row.ItemArray;
            //    if (!DogIDs.Contains(o[2].ToString()))
            //    {
            //        DogIDs.Add(o[2].ToString());
            //    }
            //}
            foreach (String id in DogIDs)
            {
                Appointment first = JDApp.query.RetrieveFirstAppointment(int.Parse(id));
                foreach (DataRow r in tmp.DataList.Table.Rows)
                {
                    object[] o = r.ItemArray;
                    string dogid = o[2].ToString();
                    DateTime time = DateTime.Parse(o[4].ToString());
                    if (dogid == id && time == first.Time)
                    {
                        // check this works
                        TimeSpan duration = TimeSpan.Parse(o[6].ToString());
                        o[6] = (duration.Add(new TimeSpan(0, 15, 0)).ToString());
                        r.ItemArray = o;
                    }
                }
            }
            //foreach (String id in DogIDs)
            //{
            //    Appointment first = JDApp.query.RetrieveFirstAppointment(int.Parse(id));
            //    foreach (DataRowView r in tmp.dataview.Items)
            //    {
            //        object[] o = r.Row.ItemArray;
            //        string dogid = o[2].ToString();
            //        DateTime time = DateTime.Parse(o[4].ToString());
            //        if(dogid == id && time == first.Time)
            //        {
            //            // check this works
            //            TimeSpan duration = TimeSpan.Parse(o[6].ToString());
            //            o[6] = (duration.Add(new TimeSpan(0,15,0)).ToString());
            //            r.Row.ItemArray[6] = (duration.Add(new TimeSpan(0, 15, 0)).ToString());
            //        }
            //    }
            //}
            data_Booking.DataList = tmp.DataList;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object[] selectedrow = data_Booking.SelectedItem;
                string TransactionID = selectedrow[0].ToString();
                string DogID = selectedrow[2].ToString();
                DateTime time = DateTime.Parse(selectedrow[4].ToString());
                if((string)selectedrow[5] == "Allergy Treatment (x4 Min)")
                {
                    string client = selectedrow[1].ToString();
                    List<object> results = new List<object> { };
                    // alternative
                    foreach(DataRowView r in data_Booking.dataview.Items)
                    {
                        object[] o = r.Row.ItemArray;
                        if (o[0].ToString() == TransactionID) { results.Add(o); }
                    }
                    //foreach (var r in data_Booking.DataList.Table.Rows)
                    //{

                    //}
                    //foreach (var c in data_Booking.DataList.FindRows(new object[] { TransactionID, client }))
                    //{
                    //    var o = c.Row.ItemArray;
                    //    results.Add(o);
                    //}
                    if (results.Count <= 4)
                    {
                        MessageBoxResult m = MessageBox.Show("There cannot be any less than 4 allergy appointments in the session. Would you like to cancel or continue booking?", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                        if (m is MessageBoxResult.Cancel)
                        {
                            foreach (object[] appt in results)
                            {
                                JDApp.query.DeleteBooking(appt[0].ToString(), appt[2].ToString(), DateTime.Parse(appt[4].ToString()));
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
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
