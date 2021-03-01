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
        /// <summary>
        /// todo null check + img source has changed
        /// </summary>
        #region Properties
        public App JDApp { get => ((App)Application.Current); }

        private string dogname;
        /// <summary>
        /// DOB of currently selected dog.
        /// </summary>
        public String DogName
        {
            get { return dogname; }
            set
            {
                if (dogname == value) return;
                dogname = value;
                this.NotifyPropertyChanged("DogName");
            }
        }

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

        private object clientname;
        /// <summary>
        /// Breed of dog to be registered.
        /// </summary>
        public object ClientName
        {
            get; set;
            //get { return clientname; }
            //set
            //{
            //    if (clientname == value) return;
            //    clientname = value;
            //    this.NotifyPropertyChanged("ClientName");
            //}
        }

        private object breedname;
        /// <summary>
        /// Breed of dog to be registered.
        /// </summary>
        public object BreedName
        {
            get { return breedname; }
            set
            {
                if (breedname == value) return;
                breedname = value;
                this.NotifyPropertyChanged("BreedName");
            }
        }

        private int breedindex;
        /// <summary>
        /// Selected index of client
        /// </summary>
        public int BreedIndex
        {
            get { return breedindex; }
            set
            {
                if (breedindex == value) return;
                breedindex = value;
                this.NotifyPropertyChanged("BreedIndex");
            }
        }

        private DateTime dob;
        /// <summary>
        /// DOB of currently selected dog.
        /// </summary>
        public DateTime DOB
        {
            get { return dob; }
            set
            {
                if (dob == value) return;
                dob = value;
                this.NotifyPropertyChanged("DOB");
            }
        }

        private string additionalinfo;
        /// <summary>
        /// Breed of dog to be registered.
        /// </summary>
        public String AdditionalInfo
        {
            get { return additionalinfo; }
            set
            {
                if (additionalinfo == value) return;
                additionalinfo = value;
                this.NotifyPropertyChanged("AdditionalInfo");
            }
        }

        #endregion
        #region Window Constructor
        /// <summary>
        /// Window Constructor
        /// </summary>
        public DogRegistration()
        {
            this.DataContext = this;
            BreedList = new ObservableCollection<string>(JDApp.query.GetBreeds());
            BreedIndex = -1;
            // change client search in future
            ClientList = new ObservableCollection<string>(JDApp.query.GetClientsString());
            ClientIndex = -1;
            DOB = DateTime.Today;
            InitializeComponent();
        }
        #endregion
        #region Methods

        #endregion
        #region Events
        private void cmb_Breed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JDApp.query.ImgSourceHasChanged(img_Dog.Img_Source))
            {
                try
                {
                    if(BreedIndex != -1)
                    {
                        String source = JDApp.query.GetBreedImageSource(BreedName.ToString());
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
            const String failedMissingData = "\u2022 All fields except for additional information must be completed.";
            try
            {
                if (DogName == "" || ClientIndex == -1 || BreedIndex == -1) { throw new NullReferenceException(); }
                JDApp.query.RegisterDog(DogName, ClientName.ToString(), BreedName.ToString(), AdditionalInfo, img_Dog.Img_Source ?? "", DOB);
            }
            catch (NullReferenceException) { MessageBox.Show(failedMissingData, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        // add to app.xaml.cs
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
