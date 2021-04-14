using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using JDGrooming.Classes;

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for StaffManagement.xaml
    /// </summary>
    public partial class StaffManagement : UserControl, INotifyPropertyChanged
    {
        public App JDApp { get => ((App)Application.Current); }

        public StaffManagement()
        {
            this.DataContext = this;
            InitializeComponent();
            ObservableCollection<Staff> StaffList = JDApp.query.GetShifts();
            data_Staff.ItemsSource = StaffList;
            cmb_Staff.ItemsSource = StaffList;
            cmb_StartTimes.ItemsSource = new ObservableCollection<TimeSpan>(Staff.Times);
            EndTimeList = new ObservableCollection<TimeSpan> (Staff.Times);
            data_Exceptions.ItemsSource = JDApp.query.GetAbsences();
            date_StartDate.BlackoutDates.AddDatesInPast();
            date_EndDate.BlackoutDates.AddDatesInPast();
        }

        private void Update_Rota(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Staff s in data_Staff.ItemsSource)
                {
                    JDApp.query.UpdateShifts(s);
                }
                MessageBox.Show("Staff Rota Successfully Updated - Please ensure you reschedule appointments outside of current shifts.");
            }
            catch { }
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // add verification of length of descp
            try
            {
                Staff s = (Staff)cmb_Staff.SelectedItem;
                DateTime starttime = date_StartDate.SelectedDate.Value.Add((TimeSpan)selected_StartTime);
                DateTime endtime = date_EndDate.SelectedDate.Value.Add((TimeSpan)cmb_EndTimes.SelectedItem);
                String description = txt_Description.Text;
                if(description.Length > 50) { MessageBox.Show("Description must be less than 50 characters"); return; }
                Absence a = new Absence(s.ID, s.Name, s.Role, starttime, endtime, description);
                JDApp.query.AddAbsence(a);
                MessageBox.Show("Absence has been added to database.", "Success", MessageBoxButton.OK);
                data_Exceptions.ItemsSource = JDApp.query.GetAbsences();
                // there is a bug when selecting a start date after end date
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("An error has occurred.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Please ensure all fields are filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception) { }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature not part of checklist");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Feature not part of checklist");
            }
            catch { MessageBox.Show("Error deleting record"); }
        }

        private void data_Exceptions_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            MessageBox.Show("Row edit end called");
        }

        private void data_Exceptions_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            MessageBox.Show("Cell edit end called");
        }

        private void date_StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (date_StartDate.SelectedDate.HasValue)
            {
                if (date_StartDate.SelectedDate.Value != DateTime.Today)
                {
                    date_EndDate.BlackoutDates.Add(new CalendarDateRange(DateTime.Today, date_StartDate.SelectedDate.Value.AddDays(-1)));
                }
                date_EndDate.DisplayDate = date_StartDate.SelectedDate.Value;
            }
         }

        private void date_EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CallFilterEndTimes();
        }

        private void cmb_StartTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CallFilterEndTimes();
        }

        private void CallFilterEndTimes()
        {
            if (date_StartDate.SelectedDate.HasValue)
            {
                if (date_EndDate.SelectedDate.HasValue)
                {
                    if (date_StartDate.SelectedDate.Value == date_EndDate.SelectedDate.Value)
                    {
                        try
                        {
                            cmb_EndTimes.Items.Filter += FilterEndTimes;
                        }
                        catch
                        { }
                    }
                }
            }
        }

        private object selected_StartTime;
        public object Selected_StartTime // debug this
        {
            get { return selected_StartTime; }
            set
            {
                if (selected_StartTime == value) return;
                selected_StartTime = value;
                this.NotifyPropertyChanged("Selected_StartTime");
            }
        }

        private ObservableCollection<TimeSpan> endtimelist;
        public ObservableCollection<TimeSpan> EndTimeList
        {
            get { return endtimelist; }
            set
            {
                if (endtimelist == value) return;
                endtimelist = value;
                this.NotifyPropertyChanged("EndTimeList");
            }
        }

        public bool FilterEndTimes(object item) // this does not fucking work
        {
            DateTime starttime = date_StartDate.SelectedDate.Value.Add((TimeSpan)selected_StartTime);
            DateTime endtime = date_EndDate.SelectedDate.Value.Add((TimeSpan)item);
            return (starttime < endtime);
        }
    }
}
