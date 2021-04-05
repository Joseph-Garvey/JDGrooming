using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Schedule
    {
        public Schedule(Staff staff, bool[] time)
        {
            this.staff = staff ?? throw new ArgumentNullException(nameof(staff));
            this.time = time ?? throw new ArgumentNullException(nameof(time));
        }

        public Staff staff { get; set; }
        public bool[] time { get; set; }

    }
}
