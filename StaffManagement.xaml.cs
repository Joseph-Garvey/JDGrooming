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
            InitializeComponent();
            data_Staff.ItemsSource = JDApp.query.GetShifts();
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
    }
}
