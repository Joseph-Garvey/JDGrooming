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

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for MenuButton.xaml
    /// </summary>
    public partial class MenuButton : UserControl, INotifyPropertyChanged
    {
        private List<MenuItem> items;
        public List<MenuItem> Items
        {
            get { return items; }
            set
            {
                if (items == value) return;
                items = value;
                this.NotifyPropertyChanged("Items");
            }
        }

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
                if (img_source == "") { img_source = null; Image = null; }
                else
                {
                    Uri uri = new Uri("pack://siteoforigin:,,," + ImageDirectory + img_source);
                    Image = new BitmapImage(uri);
                    this.NotifyPropertyChanged("Img_Source");
                }
            }
        }
        public String ImageDirectory = "/Icons/";

        public MenuButton()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        #region PropertyChanged Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }

    public class MenuItem
    {
        public MenuItem(string item)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }
        public String Item { get; set; }

    }
}
