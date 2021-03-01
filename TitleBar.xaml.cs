using System;
using System.Collections.Generic;
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

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for TitleBar.xaml
    /// </summary>
    public partial class TitleBar : UserControl, INotifyPropertyChanged
    {

        public TitleBar()
        {
            InitializeComponent();
        }

        private void btn_Close(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).MainWindow.Close();
        }

        private void btn_Fullscreen(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).ChangeWindowState(((App)Application.Current).MainWindow);
        }

        private void btn_Minimise(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).MinimiseWindow();
        }

        private void this_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((App)Application.Current).MainWindow.DragMove();
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
