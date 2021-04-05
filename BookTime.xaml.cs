﻿using JDGrooming.Classes;
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
        public String SelectedService { get; set; }

        public App JDApp { get => ((App)Application.Current); }

        private ObservableCollection<Staff> StaffList { get; set; }

        public BookTime(int ClientID, int DogID, String SelectedService)
        {
            this.DataContext = this;
            InitializeComponent();
            ObservableCollection<Staff> StaffList = JDApp.query.GetShifts();
            calendar.BlackoutDates.AddDatesInPast();
            bool[] b = new bool[48] { true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, true, true, false, };
            Schedule s = new Schedule(StaffList[0], b);
            List<Schedule> ls = new List<Schedule> { s };
            data_Availability.ItemsSource = ls;
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // update data grid
        }
    }
}
