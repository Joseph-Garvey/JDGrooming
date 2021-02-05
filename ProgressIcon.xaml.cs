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
    /// Interaction logic for ProgressIcon.xaml
    /// </summary>
    public partial class ProgressIcon : UserControl, INotifyPropertyChanged
    {
        // fix later
        private BitmapImage TrueImage { get; set; }
        public String Source_TrueImage
        {
            get => Source_TrueImage;
            set
            {

            }
        }
        public ProgressIcon()
        {
            InitializeComponent();
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
