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
using JDGrooming.Classes;

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for BookService.xaml
    /// </summary>
    public partial class BookService : UserControl, INotifyPropertyChanged
    {
        public App JDApp { get => ((App)Application.Current); }

        private TimeSpan FirstAppointmentOffset = new TimeSpan(0,15,0);

        private List<Service> services;
        public List<Service> Services
        {
            get { return services; }
            set
            {
                if (services == value) return;
                services = value;
                this.NotifyPropertyChanged("Services");
            }
        }
        /// <summary>
        /// ID of dog to be booked.
        /// </summary>
        /// <param name="ID"></param>
        public BookService(int ID)
        {
            InitializeComponent();
            this.DataContext = this;
            Services = JDApp.query.GetServices();
            if (JDApp.query.CheckFirstAppointment(ID))
            {
                foreach(Service s in Services)
                {
                    s.Duration += FirstAppointmentOffset;
                }
            }
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

    }
}
