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
    /// Interaction logic for UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : UserControl, INotifyPropertyChanged
    {
        #region Variables and Properties
        /// <summary>
        /// References the application code.
        /// </summary>
        private App JDApp { get => ((App)Application.Current); }

        private string forename;
        private String Forename
        {
            get { return forename; }
            set
            {
                if (forename == value) return;
                forename = value;
                this.NotifyPropertyChanged("Forename");
            }
        }
        private string surname;
        private String Surname
        {
            get { return surname; }
            set
            {
                if (surname == value) return;
                surname = value;
                this.NotifyPropertyChanged("Surname");
            }
        }
        /// <summary>
        /// TODO Bindable properties
        /// </summary>
        #endregion
        public UpdateClient()
        {
            this.DataContext = this;
            InitializeComponent();
            ClientView.DataList = JDApp.query.FillClientTable();
            ClientView.dataview.SelectionChanged += DataGrid_SelectionChanged;
        }

        private void VerifyText(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            CheckBox chkBox = new CheckBox();
            bool pass = false;
            switch ((textBox.Name))
            {
                case "txt_Postcode":
                    chkBox = chk_Postcode;
                    pass = Verification.VerifyPostcode(textBox.Text);
                    break;
                case "txt_Email":
                    chkBox = chk_Email;
                    pass = Verification.VerifyEmail(textBox.Text);
                    break;
                case "txt_Mobile":
                    chkBox = chk_Mobile;
                    pass = Verification.VerifyPhoneNumber(textBox.Text);
                    break;
                case "txt_HomePhone":
                    pass = Verification.VerifyPhoneNumber(textBox.Text);
                    chkBox = chk_HomePhone;
                    break;
            }
            chkBox.IsChecked = pass;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object[] selectedrow = ClientView.SelectedItem;
            }
            catch (NullReferenceException) { }
        }

        #region PropertyChanged Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
