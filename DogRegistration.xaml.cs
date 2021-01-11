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
using System.ComponentModel;

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for DogRegistration.xaml
    /// </summary>
    public partial class DogRegistration : UserControl
    {
        public DogRegistration()
        {
            InitializeComponent();
        }

        private void dp_DOB_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DateTime.Compare(dp_DOB.SelectedDate.Value, DateTime.Now) >= 0)
            {
                dp_DOB.SelectedDate = DateTime.Now.AddDays(-1);
                MessageBox.Show("Invalid DOB, must be in the past.");
            }
        }
    }
}
