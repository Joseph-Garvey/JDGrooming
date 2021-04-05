using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Appointment
    {
        public Appointment(DateTime time, int staffID, string selectedService)
        {
            Time = time;
            StaffID = staffID;
            SelectedService = selectedService ?? throw new ArgumentNullException(nameof(selectedService));
        }

        public Appointment(DateTime time, int staffID, string selectedService, TimeSpan selectedService_Duration) : this(time, staffID, selectedService)
        {
            SelectedService_Duration = selectedService_Duration;
        }

        public Appointment(int transactionID, int dogID, DateTime time, int staffID, string selectedService)
        {
            TransactionID = transactionID;
            DogID = dogID;
            Time = time;
            StaffID = staffID;
            SelectedService = selectedService ?? throw new ArgumentNullException(nameof(selectedService));
        }

        public int TransactionID { get; set; }
        public int DogID { get; set; }
        public DateTime Time { get; set; }
        public int StaffID { get; set; }
        public String SelectedService { get; set; }
        public TimeSpan SelectedService_Duration { get; set; }
        //public Staff Staff { get; set; }
        //public Service SelectedService { get; set; }

    }
}
