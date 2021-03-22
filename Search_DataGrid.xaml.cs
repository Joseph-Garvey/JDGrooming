using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for Search_DataGrid.xaml
    /// </summary>
    public partial class Search_DataGrid : UserControl, INotifyPropertyChanged
    {
        #region Variables and Properties
        private String searchtext;
        /// <summary>
        /// Search query for the data-grid displaying all dogs.
        /// </summary>
        public String SearchText
        {
            get { return searchtext; }
            set
            {
                if (searchtext == value) return;
                searchtext = value;
                this.NotifyPropertyChanged("SearchText");
                DataList.RowFilter = "Name LIKE '*" + SearchText + "*'";
            }
        }

        private DataView datalist;
        /// <summary>
        /// Dog Data
        /// </summary>
        public DataView DataList
        {
            get { return datalist; }
            set
            {
                if (datalist == value) return;
                datalist = value;
                this.NotifyPropertyChanged("DataList");
            }
        }

        public object[] SelectedItem { get => ((DataRowView)dataview.SelectedItem).Row.ItemArray; } // fix null ref exception on button click

        // old way?

        //private event SelectionChangedEventHandler selectionChanged;
        //public event SelectionChangedEventHandler SelectionChanged
        //{
        //    add
        //    {
        //        lock (dataview)
        //        {
        //            selectionChanged += value;
        //        }
        //    }
        //    remove
        //    {
        //        lock (dataview)
        //        {
        //            selectionChanged -= value;
        //        }
        //    }
        //}
        #endregion

        public Search_DataGrid()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        #region Methods
        public void Filter(object sender, FilterEventArgs e)
        {
            var item = e.Item;
        }
        public bool Filter(object item)
        {
            var items = item;
            return (((String)item).ToLowerInvariant()).Contains(SearchText.ToLowerInvariant());
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
