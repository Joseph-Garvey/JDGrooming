using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Schedule : INotifyPropertyChanged
    {
        public Schedule(Staff staff, bool[] time)
        {
            this.staff = staff ?? throw new ArgumentNullException(nameof(staff));
            this.time = time ?? throw new ArgumentNullException(nameof(time));
        }

        public Staff staff { get; set; }
        private bool[] _time;
        public bool[] time
        {
            get { return _time; }
            set
            {
                if (_time == value) return;
                _time = value;
                this.NotifyPropertyChanged("time");
            }
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
