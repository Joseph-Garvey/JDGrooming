using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Appointment
    {
        public Appointment(String DogName, DateTime time, String StaffName, Service service, int TransactionID, int DogID)
        {
            this.DogName = DogName;
            this.Time = time;
            this.StaffName = StaffName;
            this.SelectedService = service.Name;
            this.SelectedService_Duration = service.Duration;
            this.TransactionID = TransactionID;
            this.DogID = DogID;
        }
        public Appointment(DateTime time, int staffID, string selectedService)
        {
            Time = time;
            StaffID = staffID;
            SelectedService = selectedService ?? throw new ArgumentNullException(nameof(selectedService));
        }

        public Appointment(DateTime time, int staffID, string selectedService, TimeSpan selectedService_Duration, int DogID) : this(time, staffID, selectedService)
        {
            SelectedService_Duration = selectedService_Duration;
            this.DogID = DogID;
        }

        public Appointment(int transactionID, int dogID, DateTime time, int staffID, string selectedService)
        {
            TransactionID = transactionID;
            DogID = dogID;
            Time = time;
            StaffID = staffID;
            SelectedService = selectedService ?? throw new ArgumentNullException(nameof(selectedService));
        }

        public override string ToString()
        {
            return $"{DogName} - {Time:HH:mm dd/MM/yyyy} - {StaffName} - Duration: {SelectedService_Duration}";
        }

        public int TransactionID { get; set; }
        public int DogID { get; set; }
        public String DogName { get; set; }
        public DateTime Time { get; set; }
        public int StaffID { get; set; }
        public String StaffName { get; set; }
        public String SelectedService { get; set; }
        public TimeSpan SelectedService_Duration { get; set; }
        //public Staff Staff { get; set; }
        //public Service SelectedService { get; set; }

    }
}
