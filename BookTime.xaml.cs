using JDGrooming.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for BookTime.xaml
    /// </summary>
    public partial class BookTime : UserControl, INotifyPropertyChanged
    {
        public int ClientID { get; set; }
        public int DogID { get; set; }
        public Service Selected_Service { get; set; }

        public App JDApp { get => ((App)Application.Current); }

        public DateTime TransactionTime { get; set; }
        public int TransactionID = -1;

        private ObservableCollection<Staff> StaffList { get; set; }

        private DataView datalist;
        /// <summary>
        /// Dog Data
        /// </summary>
        public DataView DataList
        {
            get { return datalist; }
            set
            {
                if (datalist == value) return;
                datalist = value;
                this.NotifyPropertyChanged("DataList");
            }
        }

        private ObservableCollection<Schedule> schedules;
        public ObservableCollection<Schedule> Schedules
        {
            get { return schedules; }
            set
            {
                if (schedules == value) return;
                schedules = value;
                this.NotifyPropertyChanged("Schedules");
            }
        }

        private DateTime selected_time;
        public DateTime Selected_Time
        {
            get { return selected_time; }
            set
            {
                if (selected_time == value) return;
                selected_time = value;
                this.NotifyPropertyChanged("Selected_Time");
            }
        }

        private Staff selected_Staff;
        public Staff Selected_Staff
        {
            get { return selected_Staff; }
            set
            {
                if (selected_Staff == value) return;
                selected_Staff = value;
                this.NotifyPropertyChanged("Selected_Staff");
            }
        }

        private bool apptselected = false;

        public bool allergybooking { get; set; }

        public String Dogname { get; set; }

        private bool bookingconfirmed = false;

        public BookTime(int ClientID, int DogID, Service SelectedService, String Dogname)
        {
            TransactionTime = DateTime.Now;
            this.DataContext = this;
            Selected_Service = SelectedService;
            this.DogID = DogID;
            this.ClientID = ClientID;
            this.Dogname = Dogname;
            InitializeComponent();
            StaffList = JDApp.query.GetShifts();
            calendar.BlackoutDates.AddDatesInPast();
            if (Selected_Service.Name == "Allergy Treatment (x4 Min)")
            {
                list_Appointments.Items.Add("Note: You must book at least 4 allergy appointments within 6 weeks.");
                allergybooking = true;
            }
            else { allergybooking = false; }
            //eg data
            //bool[] b = new bool[48] { true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, };
            //Schedule s = new Schedule(StaffList[0], b);
            //List<Schedule> ls = new List<Schedule> { s };
            ////
            //data_Availability.ItemsSource = ls;
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTimetable();
        }
        private void UpdateTimetable()
        {
            if (calendar.SelectedDate.HasValue)
            {
                if(Selected_Service.Name == "Allergy Treatment (x4 Min)")
                {
                    DateTime earliest = DateTime.MaxValue;
                    DateTime latest = DateTime.MinValue;
                    if(list_Appointments.Items.Count > 1)
                    {
                        foreach (object o in list_Appointments.Items)
                        {
                            if (o is Appointment)
                            {
                                Appointment a = (Appointment)o;
                                if (earliest > a.Time) { earliest = a.Time; }
                                if(latest < a.Time) { latest = a.Time; }

                            }
                        }
                        if (calendar.SelectedDate.Value.AddDays(-42) > earliest)
                        {
                            MessageBox.Show("You must book allergy appointments within 6 weeks of the first appointment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            calendar.SelectedDate = earliest.AddDays(41);
                            return;
                        }
                        if(calendar.SelectedDate.Value.AddDays(42) < latest)
                        {
                            MessageBox.Show("You must book allergy appointments within 6 weeks of the first appointment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            calendar.SelectedDate = latest.AddDays(-41);
                            return;
                        }
                    }
                }
                Schedules = JDApp.query.GetSchedules(calendar.SelectedDate.Value, StaffList);
                data_Availability.ItemsSource = Schedules;
            }
        }

        private void data_Availability_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                if (apptselected) { return; }
                // update grid after or when changing selection // cancel // fix out of range exception
                try
                {
                    string s = data_Availability.SelectedCells[0].Column.SortMemberPath;
                    int start = 5;
                    int fin = s.IndexOf(']');
                    int blockstart = int.Parse(s.Substring(start, fin - start));
                    int blockcount = (int)(Selected_Service.Duration.TotalMinutes / 15);
                    int itemindex = Schedules.IndexOf((Schedule)data_Availability.SelectedCells[0].Item);
                    for (int i = blockstart; i < blockcount + blockstart - 1; i++) // fix change on click  /// either get item or get grid
                    {
                        if (((Schedule)(data_Availability.SelectedCells[0].Item)).time[i] == false) return; // databind this
                    }
                    for (int i = blockstart; i < blockcount + blockstart - 1; i++)
                    {
                        //((Schedule)(data_Availability.SelectedCells[0].Item)).time[i] = false;
                        Schedules[itemindex].time[i] = false;
                    }
                    data_Availability.ItemsSource = null;
                    data_Availability.ItemsSource = Schedules;
                    TimeSpan offset = new TimeSpan(0, 15*blockstart, 0);
                    Selected_Time = calendar.SelectedDate.Value + new TimeSpan(9, 15 * blockstart, 0);
                    Selected_Staff = Schedules[itemindex].staff;
                    apptselected = true;
                }
                catch { }
            }
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if (bookingconfirmed == false) { MessageBox.Show("Please confirm the current booking before adding another appointment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            bookingconfirmed = false;
            apptselected = false;
            data_Availability.ItemsSource = null;
            // set to false + do other stuff
        }

        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (bookingconfirmed == true) { MessageBox.Show("This appointment has already been confirmed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            try
            {
                if (Selected_Time == null) throw new NullReferenceException();
                if (apptselected == false) return;
                if(TransactionID == -1)
                {
                    JDApp.query.InitialiseTransaction(ClientID, TransactionTime);
                    TransactionID = JDApp.query.RetrieveTransactionID(ClientID, TransactionTime);
                }
                JDApp.query.BookAppointment(TransactionID, DogID, Selected_Time, Selected_Staff.ID, Selected_Service.Name);
                MessageBox.Show("Appointment booked.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                bookingconfirmed = true;
                Appointment a = new Appointment(Dogname, Selected_Time, this.Selected_Staff.Name, Selected_Service, TransactionID, DogID);
                list_Appointments.Items.Add(a);
            }
            catch (NullReferenceException) { MessageBox.Show("Please select a date and time.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch { MessageBox.Show("An error has occurred. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        private bool BlockExitAllergy()
        {
            if (allergybooking)
            {
                if(list_Appointments.Items.Count < 5) { return true; }
            }
            return false;
        }

        public void BlockExit()
        {
            if (BlockExitAllergy())
            {
                MessageBoxResult m = MessageBox.Show("You must make at least 4 allergy appointments. Would you like to cancel or continue booking?", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                if (m is MessageBoxResult.Cancel)
                {
                    foreach (object o in list_Appointments.Items)
                    {
                        if (o is Appointment)
                        {
                            Appointment a = (Appointment)o;
                            JDApp.query.DeleteBooking(a.TransactionID.ToString(), a.DogID.ToString(), a.Time);
                        }
                    }
                }
                else
                {
                    throw new AllergyException();
                }
            }
        }
    }
}
