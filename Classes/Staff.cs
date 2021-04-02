using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Staff : INotifyPropertyChanged
    {
        public Staff(int iD, string name)
        {
            ID = iD;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public Staff(int iD, string name, string role, TimeSpan monday_Start, TimeSpan monday_End, TimeSpan tuesday_Start, TimeSpan tuesday_End, TimeSpan wednesday_Start, TimeSpan wednesday_End, TimeSpan thursday_Start, TimeSpan thursday_End, TimeSpan friday_Start, TimeSpan friday_End) : this(iD, name)
        {
            Role = role;
            Monday_Start = monday_Start;
            Monday_End = monday_End;
            Tuesday_Start = tuesday_Start;
            Tuesday_End = tuesday_End;
            Wednesday_Start = wednesday_Start;
            Wednesday_End = wednesday_End;
            Thursday_Start = thursday_Start;
            Thursday_End = thursday_End;
            Friday_Start = friday_Start;
            Friday_End = friday_End;
        }

        public Staff(int iD, string name, string role) : this(iD, name)
        {
            Role = role ?? throw new ArgumentNullException(nameof(role));
        }

        public int ID { get; set; }
        public String Name { get; set; }
        public String Role { get; set; }

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

        public List<TimeSpan> ShiftStarts { get => new List<TimeSpan> { Monday_Start, Tuesday_Start, Wednesday_Start, Thursday_Start, Friday_Start }; }
        public List<TimeSpan> ShiftEnds { get => new List<TimeSpan> { Monday_End, Tuesday_End, Wednesday_End, Thursday_End, Friday_End }; }

        private static ObservableCollection<TimeSpan> times;
        public static ObservableCollection<TimeSpan> Times
        {
            get
            {
                if(times == null) { CreateTimes(); }
                return times;
            }
            set
            {
                if (times == value) return;
                times = value;
            }
        }

        private static void CreateTimes()
        {
                Times = new ObservableCollection<TimeSpan> { };
                for (int i = 9; i <= 17; i++)
                {
                    for (int j = 0; j <= 45; j += 15)
                    {
                        Times.Add(new TimeSpan(i, j, 0));
                    }
                }
        }

        public override string ToString()
        {
            return Name;
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
