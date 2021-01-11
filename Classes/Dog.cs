using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Dog
    {
        public int DogID { get; set; }
        public String Name { get; set; }
        public int ClientID { get; set; }
        public int BreedID { get; set; }
        public DateTime DOB { get; set; }
        public DogStatus Status { get; set; }
        public String AdditionalInfo { get; set; }
    }

    public enum DogStatus
    {
        active, // alive
        archived // dead/missing etc.
    }
}
