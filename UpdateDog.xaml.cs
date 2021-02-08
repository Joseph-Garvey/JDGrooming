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
        #region Variables
        private App JDApp { get => ((App)Application.Current); }
        private String searchtext;
        public String SearchText
        {
            get { return searchtext; }
            set
            {
                if (searchtext == value) return;
                searchtext = value;
                this.NotifyPropertyChanged("SearchText");
                data_DogList.Items.Filter += FilterDog;
            }
        }
        private DataView doglist;
        public DataView DogList
        {
            get { return doglist; }
            set
            {
                if (doglist == value) return;
                doglist = value;
                this.NotifyPropertyChanged("DogList");
            }
        }
        #endregion

        public UpdateDog()
        {
            DogList = JDApp.query.FillDogTable();
            this.DataContext = this;
            InitializeComponent();

        }

        #region Methods
        public void FilterDog(object sender, FilterEventArgs e)
        {
            var item = e.Item;
        }
        //public bool FilterDog(object item)
        //{
        //    // i could use an object model
        //    // why I shouldn't
        //    // while it is what i'm used to
        //    // its easier yes but it takes forever
        //    // and this way i do not have to deal with every data eventuality, just the text that i know is in the datagrid
        //    // this (at least in theory) makes it much easier dealing with clients many different contact detail(s) etc later
        //    var items = item;
        //    return (((String)item).ToLowerInvariant()).Contains(SearchText.ToLowerInvariant());
        //}
        #endregion
        #region Events
        private void btn_UploadImage(object sender, RoutedEventArgs e)
        {
            /// add exception handling here
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Select Dog Image",
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), // this may break
                CheckFileExists = true,
                CheckPathExists = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                /// move this to register after
                String FileName = Path.GetFileName(openFileDialog.FileName);
                string folder = Path.Combine(Environment.CurrentDirectory, @"DogImages");
                Directory.CreateDirectory(folder); // if folder does not exist create it
                string newpath = Path.Combine(folder, FileName);
                try
                {
                    File.Copy(openFileDialog.FileName, newpath);
                }
                catch { }
                img_Dog.Source = new BitmapImage(new Uri(newpath));
            }
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
