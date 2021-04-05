using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Appointment
    {
        public int TransactionID { get; set; }
        public int DogID { get; set; }
        public DateTime Time { get; set; }
        public int StaffID { get; set; }
        public String SelectedService { get; set; }
        //public Staff Staff { get; set; }
        //public Service SelectedService { get; set; }
    }
}
