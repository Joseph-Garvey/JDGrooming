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
    /// Interaction logic for ClientRegistration.xaml
    /// </summary>
    public partial class ClientRegistration : UserControl, INotifyPropertyChanged
    {
        #region Variables and Properties
        private string forename;
        public String Forename
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
        public String Surname
        {
            get { return surname; }
            set
            {
                if (surname == value) return;
                surname = value;
                this.NotifyPropertyChanged("Surname");
            }
        }
        private string firstline;
        public String FirstLine
        {
            get { return firstline; }
            set
            {
                if (firstline == value) return;
                firstline = value;
                this.NotifyPropertyChanged("FirstLine");
            }
        }
        private string secondline;
        public String SecondLine
        {
            get { return secondline; }
            set
            {
                if (secondline == value) return;
                secondline = value;
                this.NotifyPropertyChanged("SecondLine");
            }
        }
        private string postcode;
        public String Postcode
        {
            get { return postcode; }
            set
            {
                if (postcode == value) return;
                postcode = value;
                this.NotifyPropertyChanged("PostCode");
            }
        }
        private string town;
        public String Town
        {
            get { return town; }
            set
            {
                if (town == value) return;
                town = value;
                this.NotifyPropertyChanged("Town");
            }
        }
        private string email;
        public String Email
        {
            get { return email; }
            set
            {
                if (email == value) return;
                email = value;
                this.NotifyPropertyChanged("Email");
            }
        }
        private string mobile;
        public String Mobile
        {
            get { return mobile; }
            set
            {
                if (mobile == value) return;
                mobile = value;
                this.NotifyPropertyChanged("Mobile");
            }
        }
        private string homephone;
        public String HomePhone
        {
            get { return homephone; }
            set
            {
                if (homephone == value) return;
                homephone = value;
                this.NotifyPropertyChanged("HomePhone");
            }
        }
        private App JDApp { get => ((App)Application.Current); }
        #endregion

        public ClientRegistration()
        {
            this.DataContext = this;
            InitializeComponent();
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

        private void Register(object sender, RoutedEventArgs e)
        {
            const String failedMissingInfo = "\u2022 All fields marked * must be completed.";
            try
            {
                if ((Forename == null) || Forename == "" || (Surname == "") || Surname == null || (FirstLine == "") || FirstLine == null || (Postcode == "") || Postcode == null || (Town == "") || Town == null) throw new NullReferenceException();
                if(JDApp.query.RegisterClient(Forename, Surname, FirstLine, SecondLine ?? "", Postcode, Town, Email ?? "", Mobile ?? "", HomePhone ?? ""))
                {
                    Forename = "";
                    Surname = "";
                    FirstLine = "";
                    SecondLine = "";
                    Postcode = "";
                    Town = "";
                    Mobile = "";
                    Email = "";
                    HomePhone = "";
                }
            }
            catch (NullReferenceException) { MessageBox.Show(failedMissingInfo, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
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
