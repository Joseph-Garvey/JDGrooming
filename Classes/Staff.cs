using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Staff
    {
        public Staff(int iD, string name)
        {
            ID = iD;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int ID { get; set; }
        public String Name { get; set; }
    }
}
