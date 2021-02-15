using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ImageManager.xaml
    /// </summary>
    public partial class ImageManager : UserControl, INotifyPropertyChanged
    {
        #region PropertyChanged Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private BitmapImage image;
        public BitmapImage Image
        {
            get { return image; }
            set
            {
                if (image == value) return;
                image = value;
                this.NotifyPropertyChanged("Image");
            }
        }

        private String img_source;
        public String Img_Source
        {
            get { return img_source; }
            set
            {
                if (img_source == value) return;
                img_source = value;
                // fix all of this
                Uri uri = new Uri("pack://siteoforigin:,,," + ImageDirectory + img_source);
                Image = new BitmapImage(uri);
                this.NotifyPropertyChanged("Img_Source");
            }
        }
        public String ImageDirectory = "/DogImages/";

        public ImageManager()
        {
            InitializeComponent();
            this.DataContext = this;
        }
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
                String FileName = Path.GetFileName(openFileDialog.FileName);
                string folder = Path.Combine(Environment.CurrentDirectory, @"DogImages");
                Directory.CreateDirectory(folder); // if folder does not exist create it
                string newpath = Path.Combine(folder, FileName);
                try
                {
                    File.Copy(openFileDialog.FileName, newpath);
                }
                catch { }
                Img_Source = FileName;
            }
        }
    }
}
