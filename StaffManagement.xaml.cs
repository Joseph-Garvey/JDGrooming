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


        private Absence selectedabsence;
        public Absence Selectedabsence
        {
            get { return selectedabsence; }
            set
            {
                if (selectedabsence == value) return;
                selectedabsence = value;
                this.NotifyPropertyChanged("SelectedAbsence");
            }
        }

        public StaffManagement()
        {
            InitializeComponent();
            data_Staff.ItemsSource = JDApp.query.GetShifts();
            data_Exceptions.ItemsSource = JDApp.query.GetAbsences();
        }

        private void Update_Rota(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Staff s in data_Staff.ItemsSource)
                {
                    JDApp.query.UpdateShifts(s);
                }
                MessageBox.Show("Staff Rota Successfully Updated");
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
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {

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
    }
}
