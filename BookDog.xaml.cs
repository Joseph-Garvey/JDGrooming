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
using System.Windows.Shapes;

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for BookDog.xaml
    /// </summary>
    public partial class BookDog : UserControl
    {
        public App JDApp { get => ((App)Application.Current); }

        public object[] SelectedItem { get => DogData.SelectedItem; }

        public BookDog(int ID)
        {
            this.DataContext = this;
            InitializeComponent();
            DogData.DataList = JDApp.query.FillClientDogTable(ID);
        }
    }
}
