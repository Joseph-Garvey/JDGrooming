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
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for DogRegistration.xaml
    /// </summary>
    public partial class DogRegistration : UserControl, INotifyPropertyChanged
    {
        #region Properties
        public App JDApp { get => ((App)Application.Current); }
        #endregion
        #region Window Constructor
        /// <summary>
        /// Window Constructor
        /// </summary>
        public DogRegistration()
        {
            this.DataContext = this;
            BreedList = new ObservableCollection<string>(JDApp.query.GetBreeds());
            // change client search in future
            ClientList = new ObservableCollection<string>(JDApp.query.GetClientsString());
            InitializeComponent();
        }
        #endregion
        #region Methods
        private bool ImgSourceHasChanged() // fix this later with a bool type value??
        {
            return img_Dog.Img_Source != "pack://siteoforigin:,,,/Icons/add_image.png";
        }
        #endregion
        #region Events
        private void cmb_Breed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ImgSourceHasChanged())
            {
                try
                {
                    if(cmb_Breed.SelectedItem != null)
                    {
                        String source = JDApp.query.GetBreedImageSource(cmb_Breed.SelectedItem.ToString());
                        Uri imguri = new Uri(source);
                        if (imguri != null)
                        {
                            if (File.Exists(imguri.AbsolutePath)) { img_Dog.Img_Source = source; }
                        }
                    }
                }
                catch { }
            }
        }
        /// <summary>
        /// Ensures DOB is in past
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dp_DOB_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DateTime.Compare(dp_DOB.SelectedDate.Value, DateTime.Now) >= 0)
            {
                dp_DOB.SelectedDate = DateTime.Now.AddDays(-1);
                MessageBox.Show("Invalid DOB, must be in the past.");
            }
        }
        /// <summary>
        /// Registers a new dog on button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Register(object sender, RoutedEventArgs e)
        {
            // move all of this to dbaccess
            // add null checks + whitespace for name etc check user reqs
            const String failedMissingData = "\u2022 All fields except for additional information must be completed.";
            const String failedNameFormat = "\u2022 Names must consist of letters.";
            const String failedNameLength = "\u2022 Names must be less than 32 characters."; // less than or equal to?
            const String failedAdditionaInfoLength = "\u2022 Information must be less than 255 characters.";
            const String failedFileNameLength = "\u2022 Image file-path must be less than 260 characters. (Windows limit)";
            String Errors = "";
            if(txt_Name.Text == ""
                || cmb_Client.SelectedIndex == -1
                || cmb_Breed.SelectedIndex == -1) { AddToErrors(ref Errors, failedMissingData); }
            if(txt_Name.Text.Length > 32) { AddToErrors(ref Errors, failedNameLength); }
            if (txt_AdditionalInfo.Text.Length > 255) { AddToErrors(ref Errors, failedAdditionaInfoLength); }
            if (!CheckCharacters(txt_Name.Text)) { AddToErrors(ref Errors, failedNameFormat); }
            if(img_Dog.Img_Source.Length > 260) { AddToErrors(ref Errors, failedFileNameLength); }
            if (Errors != "") MessageBox.Show(Errors, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                // image is optional // add check that image still exists
                String Start = "INSERT INTO [Dog] ([Name], [DOB], [ClientID], [BreedName]";
                String End = String.Format("VALUES('{0}', '{1}', '{2}', '{3}'", txt_Name.Text, dp_DOB.SelectedDate, JDApp.query.GetClientIDFromString(cmb_Client.SelectedItem.ToString()), cmb_Breed.SelectedItem.ToString()); // find way of getting client id
                if (ImgSourceHasChanged()) { Start += ", [Image]"; End += (", '" + img_Dog.Img_Source + "'"); }
                if(txt_AdditionalInfo.Text != "") { Start += ", [AdditionalInfo]"; End += (", '" + txt_AdditionalInfo.Text + "'"); }
                JDApp.query.QueryDatabase(Start + ") " + End + ");");
                MessageBox.Show("Client registered successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        // add to app.xaml.cs
        private void AddToErrors(ref String ErrorList, String Error)
        {
            Error += Environment.NewLine;
            ErrorList += Error;
        }
        private bool CheckCharacters(String test)
        {
            foreach (Char c in test)
            {
                if (!Char.IsLetter(c)) { return false; }
            }
            return true;
        }
        #endregion
        #region Searchable ComboBox Code
        // breed
        private ObservableCollection<String> breedList;
        public ObservableCollection<string> BreedList
        {
            get { return breedList; }
            set
            {
                if (breedList == value) return;
                breedList = value;
                this.NotifyPropertyChanged("BreedList");
            }
        }
        private string breed_searchtext;
        public string Breed_searchtext
        {
            get { return breed_searchtext; }
            set
            {
                if (breed_searchtext == value) return;
                breed_searchtext = value;
                this.NotifyPropertyChanged("Breed_searchtext");
                cmb_Breed.Items.Filter += FilterBreed;
            }
        }
        public bool FilterBreed(object item)
        {
            // use regex in future
            return (((String)item).ToLowerInvariant()).Contains(Breed_searchtext.ToLowerInvariant());
        }
        // end_breed
        // client
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
            if (cmb_Breed.SelectedIndex == -1) { Breed_searchtext = ""; }
            if (cmb_Client.SelectedIndex == -1) { }
        }
        #endregion
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
