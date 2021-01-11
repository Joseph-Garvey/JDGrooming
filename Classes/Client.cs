using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Client
    {
        public int ID { get; set; }
        public String Surname { get; set; }
        public String Forename { get; set; }
        public String Email { get; set; }
        public Address Address { get; set; }
        public int HomeNo { get; set; }
        public int MobileNo { get; set; }
    }
}
