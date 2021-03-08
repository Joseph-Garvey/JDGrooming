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
    /// Interaction logic for BookClient.xaml
    /// </summary>
    public partial class BookClient : UserControl
    {
        public App JDApp { get => ((App)Application.Current); }

        public object[] SelectedItem { get => ClientData.SelectedItem; }

        public BookClient()
        {
            this.DataContext = this;
            ClientData.DataList = JDApp.query.FillClientTable();
            InitializeComponent();
        }
    }
}
