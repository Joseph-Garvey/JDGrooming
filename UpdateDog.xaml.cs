using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for UpdateDog.xaml
    /// </summary>
    public partial class UpdateDog : UserControl, INotifyPropertyChanged
    {
        #region Variables and Properties
        /// <summary>
        /// References the application code.
        /// </summary>
        private App JDApp { get => ((App)Application.Current); }

        //private SelectionChangedEventHandler selectionChanged;
        //public SelectionChangedEventHandler SelectionChanged { get => selectionChanged; set => selectionChanged = value; }

        private string dogname;
        /// <summary>
        /// Name of currently selected dog.
        /// </summary>
        public String DogName
        {
            get
            {
                return dogname;
            }
            set
            {
                if (dogname == value) return;
                dogname = value;
                this.NotifyPropertyChanged("DogName");
            }
        }
        private string breed;
        /// <summary>
        /// Breed of currently selected dog.
        /// </summary>
        public String Breed
        {
            get { return breed; }
            set
            {
                if (breed == value) return;
                breed = value;
                this.NotifyPropertyChanged("Breed");
            }
        }
        private string dob;
        /// <summary>
        /// DOB of currently selected dog.
        /// </summary>
        public String DOB
        {
            get { return dob; }
            set
            {
                if (dob == value) return;
                dob = value;
                this.NotifyPropertyChanged("DOB");
            }
        }
        private string status;
        public String Status
        {
            get { return status; }
            set
            {
                if (status == value) return;
                status = value;
                this.NotifyPropertyChanged("Status");
            }
        }
        private string doginfo;
        public String DogInfo
        {
            get { return doginfo; }
            set
            {
                if (doginfo == value) return;
                doginfo = value;
                this.NotifyPropertyChanged("DogInfo");
            }
        }
        private string breedinfo;
        public String BreedInfo
        {
            get { return breedinfo; }
            set
            {
                if (breedinfo == value) return;
                breedinfo = value;
                this.NotifyPropertyChanged("BreedInfo");
            }
        }
        private string clientname;
        public String ClientName
        {
            get { return clientname; }
            set
            {
                if (clientname == value) return;
                clientname = value;
                this.NotifyPropertyChanged("ClientName");
            }
        }
        #endregion

        public UpdateDog()
        {
            //SelectionChanged += DataGrid_SelectionChanged;
            this.DataContext = this;
            InitializeComponent();
            DogView.DataList = JDApp.query.FillDogTable();
            DogView.dataview.SelectionChanged += DataGrid_SelectionChanged;
        }

        #region Methods

        #endregion
        #region Events
        /// <summary>
        /// Verifies details and sends to database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateDog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // bind later
                object[] selectedrow = DogView.SelectedItem;
                string id = selectedrow[0].ToString();
                // Update Name, breed must be a combobox dropdown, status button, update dog info
                // maybe make client info an update later but just get this working to start off with.
                // add notification that an element has changed etc
                JDApp.query.UpdateDog(id, DogName, Breed, DogInfo);
                MessageBox.Show("Dog Updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch { }
        }
        /// <summary>
        /// Retrieves the details of selected dog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // do this stuff through bindings later?
            object[] selectedrow = DogView.SelectedItem;
            DogName = selectedrow[1].ToString();
            Breed = selectedrow[2].ToString();
            ClientName = selectedrow[3].ToString();
            DOB = selectedrow[4].ToString();
            Status = selectedrow[5].ToString();
            // info and image is in query
            String[] sqlresults = JDApp.query.GetUpdateDog(selectedrow[0].ToString());
            DogInfo = sqlresults[0] ?? "";
            BreedInfo = sqlresults[1] ?? "";
            String DogImage = sqlresults[2] ?? "";
            String BreedImage = sqlresults[3] ?? "";
            try
            {
                // fix the image string manipulation
                // change the image source into a bindable property
                String folder = Path.Combine(Environment.CurrentDirectory + @"/DogImages/");
                if (File.Exists(Path.Combine(folder, DogImage))) { img_Dog.Img_Source = DogImage; }
                else if (File.Exists(Path.Combine(folder, BreedImage))) { img_Dog.Img_Source = BreedImage; }
            }
            catch
            {
            }
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
