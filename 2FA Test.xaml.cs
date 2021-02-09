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
using JDGrooming.Classes;

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for _2FA_Test.xaml
    /// </summary>
    public partial class _2FA_Test : UserControl, INotifyPropertyChanged
    {
        private Email email = new Email("josephgarvey784@gmail.com");
        private string authcode;
        /// <summary>
        /// Name of currently selected dog.
        /// </summary>
        public String AuthCode
        {
            get
            {
                return authcode;
            }
            set
            {
                if (authcode == value) return;
                authcode = value;
                this.NotifyPropertyChanged("Authcode");
            }
        }

        public _2FA_Test()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthCode = email.Authenticate().ToString();
        }
    }
}
