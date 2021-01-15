﻿using System;
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
        public App JDApp { get => ((App)Application.Current); }

        public DogRegistration()
        {
            this.DataContext = this;
            itemlist = new ObservableCollection<string>(JDApp.query.GetBreeds());
            InitializeComponent();
        }

        private void dp_DOB_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DateTime.Compare(dp_DOB.SelectedDate.Value, DateTime.Now) >= 0)
            {
                dp_DOB.SelectedDate = DateTime.Now.AddDays(-1);
                MessageBox.Show("Invalid DOB, must be in the past.");
            }
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
            if(openFileDialog.ShowDialog() == true)
            {
                /// move this to register after
                String FileName = Path.GetFileName(openFileDialog.FileName);
                string folder = Path.Combine(Environment.CurrentDirectory, @"DogImages");
                Directory.CreateDirectory(folder); // if folder does not exist create it
                string newpath = Path.Combine(folder, FileName);
                try {
                    File.Copy(openFileDialog.FileName, newpath);
                }
                catch { }
                img_Dog.Source = new BitmapImage(new Uri(newpath));
            }
        }
        #region Searchable ComboBox Code
        private ObservableCollection<String> itemlist;
        public ObservableCollection<string> ItemList
        {
            get { return itemlist; }
            set
            {
                if (itemlist == value) return;
                itemlist = value;
                this.NotifyPropertyChanged("ItemList");
            }
        }
        private string searchtext;
        public string SearchText
        {
            get { return searchtext; }
            set
            {
                if (searchtext == value) return;
                searchtext = value;
                this.NotifyPropertyChanged("SearchText");
                cmb_Breed.Items.Filter += Filter;
            }
        }
        private void cmb_DropDownClosed(object sender, EventArgs e)
        {
            if (cmb_Breed.SelectedIndex == -1) { SearchText = ""; }
        }
        public bool Filter(object item)
        {
            // use regex in future
            return (((String)item).ToLowerInvariant()).Contains(SearchText.ToLowerInvariant());
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
