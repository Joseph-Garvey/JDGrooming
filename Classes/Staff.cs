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

        public TimeSpan Monday_Start { get; set; }
        public TimeSpan Monday_End { get; set; }

        public TimeSpan Tuesday_Start { get; set; }
        public TimeSpan Tuesday_End { get; set; }

        public TimeSpan Wednesday_Start { get; set; }
        public TimeSpan Wednesday_End { get; set; }

        public TimeSpan Thursday_Start { get; set; }
        public TimeSpan Thursday_End { get; set; }

        public TimeSpan Friday_Start { get; set; }
        public TimeSpan Friday_End { get; set; }
    }
}
