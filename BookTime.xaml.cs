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

        public BookTime(int ClientID, int DogID, Service SelectedService)
        {
            this.DataContext = this;
            Selected_Service = SelectedService;
            InitializeComponent();
            StaffList = JDApp.query.GetShifts();
            calendar.BlackoutDates.AddDatesInPast();
            //eg data
            //bool[] b = new bool[48] { true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, };
            //Schedule s = new Schedule(StaffList[0], b);
            //List<Schedule> ls = new List<Schedule> { s };
            ////
            //data_Availability.ItemsSource = ls;
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                Schedules = JDApp.query.GetSchedules(calendar.SelectedDate.Value, StaffList);
                data_Availability.ItemsSource = Schedules;
            }
        }

        private void data_Availability_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            // update grid after or when changing selection
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
                // selected time =
                // selected staff =
                // confirm
                // add to list resets it all
                // if buggy use combobox
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
    }
}
