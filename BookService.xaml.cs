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
using System.Data;
using System.Collections.ObjectModel;

namespace JDGrooming
{
    /// <summary>
    /// Interaction logic for BookService.xaml
    /// </summary>
    public partial class BookService : UserControl, INotifyPropertyChanged
    {
        public App JDApp { get => ((App)Application.Current); }

        private TimeSpan FirstAppointmentOffset = new TimeSpan(0,15,0);

        private ObservableCollection<Service> services;
        public ObservableCollection<Service> Services
        {
            get { return services; }
            set
            {
                if (services == value) return;
                services = value;
                this.NotifyPropertyChanged("Services");
            }
        }

        public int DogID { get; set; }
        public int ClientID { get; set; }
        public Service SelectedService { get => ((Service)data_Services.SelectedItem); }
        /// <summary>
        /// ID of dog to be booked.
        /// </summary>
        /// <param name="DogID"></param>
        public BookService(int DogID, int ClientID)
        {
            this.DataContext = this;
            this.DogID = DogID;
            this.ClientID = ClientID;
            this.Services = JDApp.query.GetServices();
            if (JDApp.query.CheckFirstAppointment(DogID))
            {
                foreach (Service s in Services)
                {
                    s.Duration += FirstAppointmentOffset;
                }
            }
            InitializeComponent();
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
