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
    /// Interaction logic for BookService.xaml
    /// </summary>
    public partial class BookService : UserControl
    {
        /// <summary>
        /// ID of dog to be booked.
        /// </summary>
        /// <param name="ID"></param>
        public BookService(int ID)
        {
            InitializeComponent();
        }
    }
}
