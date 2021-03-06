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
    /// Interaction logic for UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : UserControl, INotifyPropertyChanged
    {
        // switch this to an editable data grid approach if i get the time so that all aspects can be searched
        #region Variables and Properties
        /// <summary>
        /// References the application code.
        /// </summary>
        private App JDApp { get => ((App)Application.Current); }

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

        #endregion
        public UpdateClient()
        {
            this.DataContext = this;
            ClientList = new ObservableCollection<string>(JDApp.query.GetClientStringShort());
            ClientIndex = -1;
            InitializeComponent();
            // from datagrid implementation copy this to booking
            //ClientView.DataList = JDApp.query.FillClientTable();
            //ClientView.dataview.SelectionChanged += DataGrid_SelectionChanged;
        }
        #region Events

        //
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

        private void cmb_Client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ClientInfo == "" || ClientIndex == -1) { throw new NullReferenceException(); }
                String[] ClientDetails = JDApp.query.GetClientString(ClientInfo.ToString());
                Forename = ClientDetails[0];
                Surname = ClientDetails[1];
                FirstLine = ClientDetails[2];
                SecondLine = ClientDetails[3] ?? "";
                Postcode = ClientDetails[4];
                Town = ClientDetails[5];
                Email = ClientDetails[6] ?? "";
                Mobile = ClientDetails[7] ?? "";
                HomePhone = ClientDetails[8] ?? "";
            }
            catch (NullReferenceException)
            {
                Forename = "";
                Surname = "";
                FirstLine = "";
                SecondLine = "";
                Postcode = "";
                Town = "";
                Email = "";
                Mobile = "";
                HomePhone = "";
            }
        }

        private void btn_UpdateClientClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = JDApp.query.GetClientIDFromString(ClientInfo);
                JDApp.query.UpdateClient(id.ToString(), Forename, Surname, FirstLine, SecondLine, Town, Postcode, Email, Mobile, HomePhone);
                MessageBox.Show("Client Updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClientList = new ObservableCollection<string>(JDApp.query.GetClientStringShort());
                ClientIndex = -1;
            }
            catch { }
        }
        #endregion

        #region Client Search
        private string clientinfo;
        /// <summary>
        /// Info of currently selected client
        /// </summary>
        public String ClientInfo
        {
            get { return clientinfo; }
            set
            {
                if (clientinfo == value) return;
                clientinfo = value;
                this.NotifyPropertyChanged("ClientInfo");
            }
        }
        private int clientindex;
        /// <summary>
        /// Selected index of client
        /// </summary>
        public int ClientIndex
        {
            get { return clientindex; }
            set
            {
                if (clientindex == value) return;
                clientindex = value;
                this.NotifyPropertyChanged("ClientIndex");
            }
        }
        private ObservableCollection<String> clientList;
        public ObservableCollection<String> ClientList
        {
            get { return clientList; }
            set
            {
                if (clientList == value) return;
                clientList = value;
                this.NotifyPropertyChanged("ClientList");
            }
        }
        private string client_searchtext;
        public string Client_searchtext
        {
            get { return client_searchtext; }
            set
            {
                if (client_searchtext == value) return;
                client_searchtext = value;
                this.NotifyPropertyChanged("Client_searchtext");
                cmb_Client.Items.Filter += FilterClient;
            }
        }
        public bool FilterClient(object item)
        {
            return (((String)item).ToLowerInvariant()).Contains(Client_searchtext.ToLowerInvariant());
        }
        // end_client
        private void cmb_DropDownClosed(object sender, EventArgs e)
        {
            // move this to multi vm approach
            ClearSearch();
        }
        private void cmb_DropDownOpened(object sender, EventArgs e)
        {
            ClearSearch();
        }
        private void ClearSearch()
        {
            Client_searchtext = "";
        }
        #endregion

        #region PropertyChanged Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
