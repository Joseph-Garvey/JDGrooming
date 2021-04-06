using JDGrooming.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class BookTime : UserControl
    {
        public int ClientID { get; set; }
        public int DogID { get; set; }
        public Service Selected_Service { get; set; }

        public App JDApp { get => ((App)Application.Current); }

        private ObservableCollection<Staff> StaffList { get; set; }

        public List<Schedule> Schedules { get; set; }

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

        private void data_Availability_CurrentCellChanged(object sender, EventArgs e)
        {
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
                for (int i = blockstart; i < blockcount + blockstart - 1; i++) // fix change on click
                {
                    if (((Schedule)(data_Availability.SelectedCells[0].Item)).time[i] == false) return; // databind this
                }
                for (int i = blockstart; i < blockcount + blockstart - 1; i++)
                {
                    ((Schedule)(data_Availability.SelectedCells[0].Item)).time[i] = false; // needs databind + notifypropertychanged
                }
                // selected time =
                // selected staff =
                // confirm
                // add to list resets it all
                // if buggy use combobox
            }
            catch { }
        }
    }
}
